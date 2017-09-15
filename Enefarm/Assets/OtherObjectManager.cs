using HologramsLikeController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Other
{
    public string name;
    public GameObject mainObj;
    public float initialPositionZScale;
}


public class OtherObjectManager : MonoBehaviour {

    public Other[] others;


    private GameObject mainObj;
    private float initialPositionZScale;

    private GameObject mainTargetObj;

    public GameObject Target
    {
        get
        {
                return mainTargetObj;
        }

    }

    public float InitialPositionZScale
    {
        get
        {

            return initialPositionZScale;
        }

    }



public void setTarget(GameObject mainObj,float initialPositionZScale)
    {
        this.mainObj = mainObj;
        this.initialPositionZScale = initialPositionZScale;
    }

    public void Create()
    {

        var pos = Camera.main.transform.position;
        var forward = Camera.main.transform.forward;

        pos = pos + forward * initialPositionZScale;

        mainTargetObj = Instantiate(mainObj, pos, new Quaternion());
        mainTargetObj.name = "mainObj";

        mainTargetObj.transform.rotation = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0);

        //mainTargetObj.GetComponentInChildren<PositionControlManager>().Active();

        var rigidbody = mainTargetObj.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

}
