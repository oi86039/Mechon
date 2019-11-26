using DisablerAi;
using DisablerAi_Implemented;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    /**Initialize AI*/

    [Header("AI")]
    public Robot robot;                              /**Robot properties (health, patrol points, etc)*/

    public EnemyHead head;                          /**Get the robot's head component for the AI*/
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
    public int health; //replace with ai.health

    public SkinnedMeshRenderer jointMeshRenderer;   /**mesh renderer to turn body blue upon being hit*/

    private Rigidbody[] jointRigidBodies;                   /**List of joints for the robot's ragdoll (Used to register hits)*/
    public Color defaultColor;

    // public Animator anim;
    // public bool dead; //Is the enemy ragdoll/dead? REPLACE with ai.dead

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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
            , new Location(transform.position) //First target is unset (main position of the robot)
            , new Location(patrolStart.transform.position)
            , new Location(patrolEnd.transform.position)
            , pois
            , head.head
            , health
            );

        //Create Player
        player = playerObj.GetComponent<playerCollisions>().player;

        //Create AI
        ai = new RobotAi(robot, player);

        #endregion Init AI

        //Init common properties
        jointRigidBodies = GetComponentsInChildren<Rigidbody>();
        jointMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        defaultColor = jointMeshRenderer.material.color;

        //  anim = GetComponent<Animator>();
        // dead = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
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
                break;

            case RobotAiState.AlertCallHeadQuarters:
                break;

            case RobotAiState.AlertAttack:
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
                break;

            case RobotAiState.Disabled:
                //Ragdoll
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

    #region Old Shot

    //Old functions
    //void Die() //Call when hitting the body
    //{
    //    health--;
    //    if (!dead)
    //        jointMeshRenderer.material.color = Color.cyan;

    //    if (health <= 0)
    //    {
    //        health = 0;
    //        foreach (Rigidbody rb in jointRigidBodies)
    //        {
    //            rb.isKinematic = false; //Enable Ragdoll
    //                                    // anim.enabled = false;

    //        }
    //        dead = true;
    //    }
    //}

    //void DieNow() //Call when hitting the head
    //{
    //    health = 0;
    //    if (!dead)
    //        jointMeshRenderer.material.color = Color.cyan;
    //    foreach (Rigidbody rb in jointRigidBodies)
    //    {
    //        rb.isKinematic = false; //Enable Ragdoll
    //                                // anim.enabled = false;

    //    }
    //    dead = true;

    //}

    #endregion Old Shot
}