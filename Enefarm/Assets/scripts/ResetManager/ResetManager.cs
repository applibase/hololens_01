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

            if (objectManager.Target.name.Equals("mainObj"))
            {
                objectManager.Change();
            }

            var target = objectManager.Target;

            var pos = Camera.main.transform.position;
            var forward = Camera.main.transform.forward * objectManager.InitialPositionZScale;
            target.transform.position = pos + forward;

            target.transform.rotation = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0);

            if (target.transform.Find("TransformController") != null)
            {
                var controllCube = target.transform.Find("TransformController")
                    .transform.Find("PositionControlManager")
                    .transform.Find("ControlCube")
                    .transform.gameObject;

                controllCube.transform.rotation = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0);
            }

            var rigidbody = target.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                //rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                rigidbody.useGravity = false;
            }
            
        }
    }

    


}
