using DisablerAi;
using DisablerAi_Implemented;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    /**Initialize AI*/

    [Header("AI")]
    //public bool inactive;   /**Is the Ai even on?*/
    public Robot robot;                              /**Robot properties (health, patrol points, etc)*/
    public float walkSpeed;
    public float runSpeed;
    bool firing; /**Is the robot firing his gun?*/

    FieldOfView fov;                             /**Field of view script allowing for visual detection*/

    EnemyHead head;                          /**Get the robot's head component for the AI*/
    public GameObject playerObj;                     /**Reference to the player*/

    public RobotAi ai;                                      /**Main AI state machine*/
    private Player player;                                   /**Reference to player for ai code*/

    [Header("Paths")]
    NavMeshAgent agent;                      /**Navmesh agent allowing for built in A* pathfinding movement*/
    public Transform patrolStart;                    /**Point A for AI path*/

    public Transform patrolEnd;                      /**Point B for AI path*/
    public Transform[] pointsOfInterest;             /**A list of points for the AI to go to should the player be hidden during alert*/
    int nextPOI;                                /**Next point of interest to go to*/

    private List<Location> pois;                             /**Points of interest list for ai code*/

    [Header("Robot Properties")]

    public Transform firePoint; //Point in which the projectile of the robot will fire from
    public GameObject bullet;

    public float rotateOffset = 31.5f; //Offset for rotation

    public int health;                            //replace with ai.health

    SkinnedMeshRenderer jointMeshRenderer;       /**mesh renderer to turn body blue upon being hit*/

    public Rigidbody[] jointRigidBodies;                   /**List of joints for the robot's ragdoll (Used to register hits)*/
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
                //agent.isStopped = true;
                agent.speed = runSpeed;

                //Rotate towards player
                // agent.updateRotation = false;
                var targetRotation = Quaternion.LookRotation(playerObj.transform.position - transform.position);
                targetRotation *= Quaternion.Euler(0, rotateOffset, 0);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 4 * Time.deltaTime);

                //Play Animation
                anim.SetFloat("Unalert Transition", 0);
                anim.SetTrigger("Alert!");
                StartCoroutine(AnimatorTransition(true));
                break;

            case RobotAiState.AlertCallHeadQuarters:
                //Rotate towards player
                var targetRotation2 = Quaternion.LookRotation(playerObj.transform.position - transform.position);
                targetRotation2 *= Quaternion.Euler(0, rotateOffset, 0);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation2, 4 * Time.deltaTime);

                //for each robot in scene, state = alert
                //agent.isStopped = true;
                StartCoroutine(AlertCallHeadQuarters());
                break;

            case RobotAiState.AlertAttack:
                nextPOI = 0;
                agent.ResetPath();
                //Rotate towards player
                var targetRotation3 = Quaternion.LookRotation(playerObj.transform.position - transform.position);
                targetRotation3 *= Quaternion.Euler(0, rotateOffset, 0);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation3, 4 * Time.deltaTime);

                if (!firing)
                    StartCoroutine(BurstFire());
                break;

            case RobotAiState.AlertReposition:
                //Rotate towards player
                var targetRotation4 = Quaternion.LookRotation(playerObj.transform.position - transform.position);
                targetRotation4 *= Quaternion.Euler(0, rotateOffset, 0);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation4, 4 * Time.deltaTime);
                agent.SetDestination(robot.Target.Position);


                break;

            case RobotAiState.AlertFollowUp:
                //Rotate towards last seen position

                //fov.gameObject.transform.rotation *= Quaternion.Euler(0, rotateOffset, 0);
                var targetRotation5 = Quaternion.LookRotation(ai.Robot.Target.Position - transform.position);
                targetRotation5 *= Quaternion.Euler(0, rotateOffset, 0);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation5, 4 * Time.deltaTime);

                //Move towards last seen position
                agent.SetDestination(ai.Robot.Target.Position);

                //If we got to the player and we don't find him, go to points of interest
                if (ai.Robot.ReachedTarget() && ai.Robot.CanSee() == false)
                {
                    if (pointsOfInterest.Length > 0 && nextPOI < pointsOfInterest.Length)
                    {
                        ai.Robot.Target.Position = pointsOfInterest[nextPOI].position;
                        nextPOI++;
                    }
                    else //If we explored all points of interest without finding the player, give up.
                        ai.State = RobotAiState.Patrol;
                }

                break;

            case RobotAiState.Patrol:
                //Reset Alert Animations
                anim.SetFloat("Alert Transition", 0);
                anim.SetTrigger("Unalert");

                StartCoroutine(AnimatorTransition(false));

                agent.speed = walkSpeed;

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
                if (anim.enabled == true)
                    anim.SetTrigger("Hurt");
                robot.PlayingAnimation = RobotAnimation.HurtStagger;
                break;

            case RobotAiState.Disabled:
                //Ragdoll
                anim.enabled = false;
                agent.enabled = false;
                robot.PlayingAnimation = RobotAnimation.RagDoll;
                foreach (Rigidbody rb in jointRigidBodies)
                {
                    rb.isKinematic = false; //Enable Ragdoll
                                            // anim.enabled = false;
                }
                Destroy(this);
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

        Enemy[] robotList = FindObjectsOfType<Enemy>();

        //Set all robots to alert
        foreach (Enemy r in robotList)
        {
            if (r.ai.State != RobotAiState.Alert && r.ai.State != RobotAiState.AlertAttack &&
                r.ai.State != RobotAiState.AlertCallHeadQuarters && r.ai.State != RobotAiState.AlertFollowUp &&
                r.ai.State != RobotAiState.AlertReposition)

                r.ai.calledForAlert = true;
            r.robot.PlayingAnimation = RobotAnimation.None;
        }

        yield return new WaitForSeconds(0.02f);

        //Set call off
        foreach (Enemy r in robotList)
        {
            r.ai.calledForAlert = false;
            r.robot.PlayingAnimation = RobotAnimation.None;
        }

        robot.PlayingAnimation = RobotAnimation.None;
    }

    IEnumerator BurstFire()
    {
        firing = true;
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("FIRE");
            var b = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            bullet bb = b.GetComponent<bullet>();
            bb.player = playerObj;
            bb.homing = true;
            i++;
            yield return new WaitForSeconds(0.08f);
        }
        Debug.Log("Burst cooldown...");
        yield return new WaitForSeconds(0.7f);
        firing = false;
    }

    IEnumerator AnimatorTransition(bool inAlert)
    {

        if (inAlert)
        {
            while (anim.GetFloat("Alert Transition") < 1)
            {
                anim.SetFloat("Alert Transition", anim.GetFloat("Alert Transition") + 0.012f);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (anim.GetFloat("Unalert Transition") < 1)
            {
                anim.SetFloat("Unalert Transition", anim.GetFloat("Unalert Transition") + 0.012f);
                yield return new WaitForEndOfFrame();
            }
        }
    }

}