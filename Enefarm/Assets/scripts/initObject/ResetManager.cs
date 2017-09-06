using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class ResetManager : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        var objectManager = GameObject.Find("ObjectManager").GetComponent<TargetObjectManager>();

        if (objectManager != null)
        {
            var target = objectManager.Target;

            var pos = Camera.main.transform.position;
            var forward = Camera.main.transform.forward;

            target.transform.position = pos + forward;

            var rigidbody = target.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            
        }
    }


}
