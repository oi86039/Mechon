using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody[] jointRigidBodies;
    public SkinnedMeshRenderer jointMeshRenderer;
    public Color defaultColor;
    //public Animator anim;
    public int health = 5;
    public bool dead; //Is the enemy ragdoll/dead?

    // Start is called before the first frame update
    void Start()
    {
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
