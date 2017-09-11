using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{

    private Vector3 collisionPosition = Vector3.zero;
    private bool collision = false;

    public bool isCollision
    {
        get
        {
            return collision;
        }

        set
        {
            collision = value;
        }
    }


    public Vector3 cPosition
    {
        get
        {
            return collisionPosition;
        }
        
    } 
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        this.collision = true;
        collisionPosition = collision.transform.position;
    }
    void OnCollisionExit(Collision collision)
    {
        this.collision = false;
        collisionPosition = Vector3.zero;
    }

    void OnCollisionStay(Collision collision)
    {
        this.collision = true;
        collisionPosition = collision.transform.position;
    }

}
