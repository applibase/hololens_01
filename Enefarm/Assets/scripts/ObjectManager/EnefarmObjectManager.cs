using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HologramsLikeController;

[System.Serializable]
public class Enefarm
{
    public string name;
    public GameObject changeObj;
    public GameObject mainObj;
    public float initialPositionZScale;
}

public class EnefarmObjectManager : MonoBehaviour
{

    public Enefarm[] enefarms;

    private GameObject changeObj;
    private GameObject mainObj;
    private float initialPositionZScale;

    private GameObject changeTargetObj;
    private GameObject mainTargetObj;

    private Vector3 mainTargetScale;


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

    public float InitialPositionZScale
    {
        get
        {

            return initialPositionZScale;
            

        }

    }

    public void SetTarget(GameObject mainObj,GameObject changeObj, float initialPositionZScale)
    {
        this.mainObj = mainObj;
        this.changeObj = changeObj;
        this.initialPositionZScale = initialPositionZScale;
    }

    public void Create()
    {
        CreateChangeTarget();
        CreateMainTarget();
    }

    private void CreateChangeTarget()
    {
        var pos = Camera.main.transform.position;
        var forward = Camera.main.transform.forward;

        pos = pos +  forward * initialPositionZScale;

        changeTargetObj = Instantiate(changeObj, pos, new Quaternion());
        changeTargetObj.name = "changedObj";

        changeTargetObj.transform.rotation = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0);

        var rigidbody = changeTargetObj.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        //changeTargetObj.GetComponentInChildren<PositionControlManager>().Active();
    }

    private void CreateMainTarget()
    {

        var pos = Camera.main.transform.position;
        var forward = Camera.main.transform.forward;
        pos = pos + forward * initialPositionZScale;

        mainTargetObj = Instantiate(mainObj, pos, new Quaternion());
        mainTargetObj.name = "mainObj";
        var rigidbody = mainTargetObj.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbody.useGravity = true;
        }

        mainTargetObj.transform.rotation = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0);

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
