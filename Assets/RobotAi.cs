using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAi : MonoBehaviour
{
    public GameObject bullet;
    public GameObject robot;
    public GameObject player;

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
        return DistanceBetweenRobotAndPlayer() <= 100f;
    }

    void KeepLookingAtPlayer()
    {
        robot.transform.rotation = Quaternion.Slerp(transform.rotation, RotationTowardsPlayer(), Time.deltaTime * 1f);
    }

    void ShootAtPlayer()
    {
        var fired = Instantiate(bullet, robot.transform.position, RotationTowardsPlayer() * robot.transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateShoot", 10, 10);
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
    }
}
