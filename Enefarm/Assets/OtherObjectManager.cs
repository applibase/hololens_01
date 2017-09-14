using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Other
{
    public string name;
    public GameObject mainObj;
    public float initialPositionZ;
}


public class OtherObjectManager : MonoBehaviour {

    public Other[] others;


    private GameObject mainObj;
    private float initialPositionZ;

    private GameObject mainTargetObj;

    public GameObject Target
    {
        get
        {
                return mainTargetObj;
        }

    }

    public void setTarget(GameObject mainObj,float initialPositionZ)
    {
        this.mainObj = mainObj;
        this.initialPositionZ = initialPositionZ;
    }

    public void Create()
    {
        var pos = Camera.main.transform.forward;

        pos.z = pos.z + initialPositionZ;
        mainTargetObj = Instantiate(mainObj, pos, new Quaternion());
        mainTargetObj.name = "mainObj";
        var rigidbody = mainTargetObj.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

}
