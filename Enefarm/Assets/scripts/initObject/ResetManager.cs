using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class ResetManager : MonoBehaviour {

    public float resetPositionY;
    public float resetPositionZ;
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

            if (objectManager.Target.name.Equals("mainObj"))
            {
                objectManager.ChangeTarget();
            }

            var target = objectManager.Target;

            var pos = Camera.main.transform.position;
            var forward = Camera.main.transform.forward;

            forward.y = resetPositionY;
            forward.z = resetPositionZ;
            
            target.transform.position = pos + forward;

            var rotation = target.transform.rotation;

            rotation.y = 0;

            target.transform.rotation = rotation;

            var rigidbody = target.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                //rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                rigidbody.useGravity = false;
            }
            
        }
    }


}
