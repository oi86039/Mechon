using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAi : MonoBehaviour
{
    public GameObject bullet;
    public GameObject robot;
    public GameObject player;
    int timeout = 100;

    private GameObject launchedProjectile = null;

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

    void KeepLookingAtPlayer()
    {
        robot.transform.rotation = Quaternion.Slerp(transform.rotation, RotationTowardsPlayer(), Time.deltaTime * 1f);
    }

    void ShootAtPlayer()
    {
        var bulletSpawnPoint = Transform.Instantiate(robot.transform);
        bulletSpawnPoint.rotation = RotationTowardsPlayer() * bulletSpawnPoint.rotation;

        if (timeout <= 0)
        {
            timeout = 100;
        }


        //Draw debug laser pointer
        if (timeout == 100)
            launchedProjectile = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);



        timeout -= 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        if (DistanceBetweenRobotAndPlayer() > 100)
        {
            // Player is too far away. Ignore them.
            return;
        }

        // Make sure we stay pointed at the player
        KeepLookingAtPlayer();

        // If player is within our FOV, attack them
        if (IsLookingAtPlayer())
        {

            Debug.Log("Looking at player");

            //ShootAtPlayer();
        } 
    }
}
