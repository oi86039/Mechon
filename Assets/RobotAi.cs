using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAi : MonoBehaviour
{
    public GameObject bullet;
    public GameObject robot;
    public GameObject player;


    [Header("Omar's Vars")]
    public Enemy enemyScript;

    public Transform firePoint; //Point in which the projectile of the robot will fire from

    public float range; //maximum distance where robot will look towards player

    Quaternion RotationTowardsPlayer()
    {
        Vector3 lookVector = player.transform.position - robot.transform.position;
        lookVector.y = robot.transform.position.y;
        return Quaternion.LookRotation(lookVector);
    }

    bool IsLookingAtPlayer()
    {

        Vector3 dirFromAtoB = (player.transform.position - robot.transform.position).normalized;
        float dotProd = Vector3.Dot(dirFromAtoB, robot.transform.forward);
        return dotProd > 0.9;
    }

    float DistanceBetweenRobotAndPlayer()
    {
        return (robot.transform.position - player.transform.position).magnitude;
    }

    bool IsPlayerInRange()
    {
        return DistanceBetweenRobotAndPlayer() <= range;
    }

    void KeepLookingAtPlayer()
    {
        robot.transform.rotation = Quaternion.Slerp(transform.rotation, RotationTowardsPlayer(), Time.deltaTime * 1f);
    }

    void ShootAtPlayer()
    {
        //var fired = Instantiate(bullet, robot.transform.position, RotationTowardsPlayer() * robot.transform.rotation);
        var fired = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdateShoot", 10, 10);
        InvokeRepeating("UpdateShoot", 1.1f, 1.1f);

        //Get enemyScript for joints.
        enemyScript = GetComponent<Enemy>();
    }

    /// <summary>
    /// Handle the update loop for shooting at the player.
    /// </summary>
    void UpdateShoot()
    {
        if (IsPlayerInRange() && IsLookingAtPlayer())
        {
            Debug.Log("UpdateShoot at player");
            ShootAtPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (IsPlayerInRange())
        {
            // Make sure we stay pointed at the player
            KeepLookingAtPlayer();
        }

        //Destroy this AI script if enemy is dead
        if (enemyScript.dead)
        {
            Destroy(this);
        }
    }
}
