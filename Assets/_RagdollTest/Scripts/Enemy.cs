using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DisablerAi;
using DisablerAi_Implemented;

public class Enemy : MonoBehaviour
{
    /**Initialize AI*/
    public Robot robot; /**Robot properties (health, patrol points, etc)*/
    RobotAi ai; /**Main AI state machine*/
    public GameObject playerObj /**Reference to the player*/;


    Rigidbody[] jointRigidBodies; /**List of joints for the robot's ragdoll (Used to register hits)*/
    public SkinnedMeshRenderer jointMeshRenderer; /**mesh renderer to turn body blue upon being hit*/
    public Color defaultColor;
    //public Animator anim;
    public int health = 5; //replace with ai.health
    public bool dead; //Is the enemy ragdoll/dead? REPLACE with ai.dead

    // Start is called before the first frame update
    void Start()
    {
        //Create AI
        ai = new RobotAi(robot, playerObj.GetComponent<playerCollisions>().playerScript);



        jointRigidBodies = GetComponentsInChildren<Rigidbody>();
        jointMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        defaultColor = jointMeshRenderer.material.color;

        //  anim = GetComponent<Animator>();
        dead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Go to default color
        jointMeshRenderer.material.color = Color.Lerp(jointMeshRenderer.material.color, defaultColor, 0.1f);

        //Update ai state
        ai.Think();

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
                break;
            case RobotAiState.PatrolMarchToStart:
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
            case RobotAiState.Disabled: //dead
                DieNow();
                break;
        }
    }
    void Die() //Call when hitting the body
    {
        health--;
        if (!dead)
            jointMeshRenderer.material.color = Color.cyan;

        if (health <= 0)
        {
            health = 0;
            foreach (Rigidbody rb in jointRigidBodies)
            {
                rb.isKinematic = false; //Enable Ragdoll
                                        // anim.enabled = false;

            }
            dead = true;
        }
    }

    void DieNow() //Call when hitting the head
    {
        health = 0;
        if (!dead)
            jointMeshRenderer.material.color = Color.cyan;
        foreach (Rigidbody rb in jointRigidBodies)
        {
            rb.isKinematic = false; //Enable Ragdoll
                                    // anim.enabled = false;

        }
        dead = true;

    }

}

