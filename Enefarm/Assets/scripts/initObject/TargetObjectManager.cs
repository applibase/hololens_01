using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetObjectManager : MonoBehaviour
{

    public GameObject initialObj;
    public GameObject mainObj;

    private GameObject changeTargetObj;
    private GameObject mainTargetObj;

    private Vector3 mainTargetScale;

    public float initialPositionZ;
    private bool isChanged;

    public GameObject Target
    {
        get
        {
            if (isChanged)
            {
                return mainTargetObj;
            }
            else
            {
                return changeTargetObj;
            }
        }

    }

    // Use this for initialization
    void Start()
    {
        CreateChangeTarget();
        CreateMainTarget();
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateChangeTarget()
    {
        var pos = Camera.main.transform.forward;

        pos.z = pos.z + initialPositionZ;
        changeTargetObj = Instantiate(initialObj, pos, new Quaternion());
        changeTargetObj.name = "changedObj";
        var rigidbody = changeTargetObj.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

    }

    private void CreateMainTarget()
    {
        var pos = Camera.main.transform.forward;

        pos.z = pos.z + initialPositionZ;
        mainTargetObj = Instantiate(mainObj, pos, new Quaternion());
        mainTargetObj.name = "mainObj";
        var rigidbody = mainTargetObj.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbody.useGravity = true;
        }

        mainTargetObj.SetActive(false);

        mainTargetScale = mainTargetObj.transform.Find("BatteryUnit").gameObject.transform.lossyScale;
    }

    public void ChangeTarget()
    {
        if (isChanged)
        {
            mainTargetObj.SetActive(false);
            changeTargetObj.SetActive(true);

            var position = mainTargetObj.transform.position;
            position.y = position.y - mainTargetScale.y / 2f;

            changeTargetObj.transform.position = position;
            changeTargetObj.transform.rotation = mainTargetObj.transform.rotation;
            isChanged = false;

        }
        else
        {
            mainTargetObj.SetActive(true);
            changeTargetObj.SetActive(false);

            var position = changeTargetObj.transform.position;
            position.y = position.y + mainTargetScale.y / 2f;

            mainTargetObj.transform.position = position;
            mainTargetObj.transform.rotation = changeTargetObj.transform.rotation;
            isChanged = true;
        }
       

        
    }
}
