using DisablerAi;
using DisablerAi_Implemented;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    /**Initialize AI*/

    [Header("AI")]
    public Robot robot;                              /**Robot properties (health, patrol points, etc)*/
    public float walkSpeed;
    public float runSpeed;
    bool firing; /**Is the robot firing his gun?*/

    FieldOfView fov;                             /**Field of view script allowing for visual detection*/

    EnemyHead head;                          /**Get the robot's head component for the AI*/
    public GameObject playerObj;                     /**Reference to the player*/

    private RobotAi ai;                                      /**Main AI state machine*/
    private Player player;                                   /**Reference to player for ai code*/

    [Header("Paths")]
    NavMeshAgent agent;                      /**Navmesh agent allowing for built in A* pathfinding movement*/
    public Transform patrolStart;                    /**Point A for AI path*/

    public Transform patrolEnd;                      /**Point B for AI path*/
    public Transform[] pointsOfInterest;             /**A list of points for the AI to go to should the player be hidden during alert*/

    private List<Location> pois;                             /**Points of interest list for ai code*/

    [Header("Robot Properties")]

    public Transform firePoint; //Point in which the projectile of the robot will fire from
    public GameObject bullet;


    public int health;                            //replace with ai.health

    SkinnedMeshRenderer jointMeshRenderer;       /**mesh renderer to turn body blue upon being hit*/

    private Rigidbody[] jointRigidBodies;                   /**List of joints for the robot's ragdoll (Used to register hits)*/
    Color defaultColor;

    Animator anim;

    private void Awake()
    {
        //Init common properties
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        anim = GetComponent<Animator>();

        head = GetComponentInChildren<EnemyHead>();
        fov = GetComponentInChildren<FieldOfView>();
        jointRigidBodies = GetComponentsInChildren<Rigidbody>();
        jointMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        defaultColor = jointMeshRenderer.material.color;
    }

    // Start is called before the first frame update
    private void Start()
    {
        #region Init AI

        //Convert points of interest to Location objects
        pois = new List<Location>();
        foreach (Transform t in pointsOfInterest)
        {
            pois.Add(new Location(t.position));
        }

        //Create Robot
        robot = new Robot(
            new Location(transform.position) //Location of the robot
            , new Location(patrolEnd.transform.position) //First target is PatrolEnd Point
            , new Location(patrolStart.transform.position)
            , new Location(patrolEnd.transform.position)
            , pois
            , head.head
            , health
            , fov
            );

        //Create Player
        player = playerObj.GetComponent<playerCollisions>().player;

        //Create AI
        ai = new RobotAi(robot, player);

        #endregion Init AI        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Update animation (patrol)
        anim.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        //Go to default color
        jointMeshRenderer.material.color = Color.Lerp(jointMeshRenderer.material.color, defaultColor, 0.1f);

        //Update ai positions here
        ai.Robot.Location = new Location(transform.position);

        //Update ai state
        ai.Think();

        Debug.Log(ai.State);

        //Check AI state and adjust behavior accordingly
        switch (ai.State)
        {
            case RobotAiState.Start:
                break;

            case RobotAiState.Inactive:
                break;

            case RobotAiState.Alert:
                agent.ResetPath();
                agent.speed = runSpeed;
                transform.LookAt(playerObj.transform.position);
                //Play Animation
                anim.SetTrigger("Alert!");
                anim.SetFloat("Alert Transition", anim.GetFloat("Alert Transition") + 0.012f);
                break;

            case RobotAiState.AlertCallHeadQuarters:
                //for each robot in scene, state = alert
                StartCoroutine(AlertCallHeadQuarters());
                break;

            case RobotAiState.AlertAttack:
                if (!firing)
                    StartCoroutine(BurstFire());
                break;

            case RobotAiState.AlertReposition:
                break;

            case RobotAiState.AlertFollowUp:
                break;

            case RobotAiState.Patrol:
                break;

            case RobotAiState.PatrolMarchToEnd:
                agent.SetDestination(robot.Target.Position);
                break;

            case RobotAiState.PatrolMarchToStart:
                agent.SetDestination(robot.Target.Position);
                break;

            case RobotAiState.PatrolLookAround:
                robot.PlayingAnimation = RobotAnimation.LookAround;
                break;

            case RobotAiState.Suspicion:
                break;

            case RobotAiState.SuspicionCallHeadQuarters:
                break;

            case RobotAiState.SuspicionFollowUp:
                break;

            case RobotAiState.SuspicionLookAround:
                break;

            case RobotAiState.SuspicionShrugOff:
                break;

            case RobotAiState.Searching:
                break;

            case RobotAiState.SearchingFollowUpPointOfInterest:
                break;

            case RobotAiState.SearchingLookAroundPointOfInterest:
                break;

            case RobotAiState.SearchingFollowUpPlayerLastSeen:
                break;

            case RobotAiState.SearchingLookAroundPlayerLastSeen:
                break;

            case RobotAiState.HeldUp:
                agent.ResetPath();
                robot.PlayingAnimation = RobotAnimation.None;
                break;

            case RobotAiState.HeldUpDemandMarkAmmo:
                break;

            case RobotAiState.HeldUpDemandMarkEnemies:
                break;

            case RobotAiState.HeldUpRefuse:
                break;

            case RobotAiState.HeldUpGetDown:
                break;

            case RobotAiState.Hurt:
                robot.PlayingAnimation = RobotAnimation.HurtStagger;
                break;

            case RobotAiState.Disabled:
                //Ragdoll
                robot.PlayingAnimation = RobotAnimation.RagDoll;
                foreach (Rigidbody rb in jointRigidBodies)
                {
                    rb.isKinematic = false; //Enable Ragdoll
                                            // anim.enabled = false;
                }
                break;
        }
    }

    private void Shot() //Call when hitting the body
    {
        ai.Robot.Shot = true;
        //Flash blue if hit
        if (ai.State != RobotAiState.Disabled)
            jointMeshRenderer.material.color = Color.cyan;
    }

    IEnumerator AlertCallHeadQuarters()
    {
        robot.PlayingAnimation = RobotAnimation.AlertCallHeadQuarters;
        yield return new WaitForSeconds(0.02f);
        robot.PlayingAnimation = RobotAnimation.None;
    }

    IEnumerator BurstFire()
    {
        firing = true;
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("FIRE");
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            i++;
            yield return new WaitForSeconds(0.08f);
        }
        Debug.Log("Burst cooldown...");
        yield return new WaitForSeconds(0.7f);
        firing = false;
    }

}