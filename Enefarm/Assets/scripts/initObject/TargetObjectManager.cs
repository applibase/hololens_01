using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetObjectManager : MonoBehaviour
{

    public GameObject initialObj;

    [System.NonSerialized]
    private GameObject targetObj;

    public GameObject Target
    {
        get
        {
            return targetObj;
        }

        set
        {
            targetObj = value;
        }
    }
    // Use this for initialization
    void Start()
    {

        var pos = Camera.main.transform.forward;
        targetObj = Instantiate(initialObj, pos, new Quaternion());

        var rigidbody = targetObj.GetComponent<Rigidbody>();
        //obj.GetComponent<Rigidbody>().position = Vector3.zero;
        //obj.GetComponent<Rigidbody>().freezeRotation =
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;


    }

    // Update is called once per frame
    void Update()
    {

    }



}
