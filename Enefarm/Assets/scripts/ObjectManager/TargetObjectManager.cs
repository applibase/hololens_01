using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TargetMode
{
    Enefarm, Other
}

public class TargetObjectManager : MonoBehaviour {

    public TargetMode mode;
    public string objName;

    private EnefarmObjectManager enefarmObjectManager;
    private OtherObjectManager otherObjectManager;

    public GameObject Target
    {
        get
        {
            switch (mode)
            {
                case TargetMode.Enefarm:
                    return enefarmObjectManager.Target;
                case TargetMode.Other:
                    return otherObjectManager.Target;
                default:
                    return null;
            }

        }
    }

    public float InitialPositionZScale
    {
        get
        {
            switch (mode)
            {
                case TargetMode.Enefarm:
                    return enefarmObjectManager.InitialPositionZScale;
                case TargetMode.Other:
                    return otherObjectManager.InitialPositionZScale;
                default:
                    return 0f;
            }

        }
    }

    // Use this for initialization
    void Start () {

        enefarmObjectManager = GetComponent<EnefarmObjectManager>();
        otherObjectManager = GetComponent<OtherObjectManager>();

        Create();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Create()
    {
        switch (mode)
        {
            case TargetMode.Enefarm:

                var enefarms = enefarmObjectManager.enefarms;
                var enefarm = enefarms.ToList()
                    .Where(e => e.name.Equals(this.objName))
                    .FirstOrDefault();

                if (enefarm == null)
                {
                    Debug.LogError("TargetObjectManager-Start: objName = " + this.objName  + " に一致するEnefarmオブジェクトが見つかりません");
                    return;
                }

                enefarmObjectManager.SetTarget(enefarm.mainObj, enefarm.changeObj, enefarm.initialPositionZScale);

                enefarmObjectManager.Create();
                enefarmObjectManager.Target.SetActive(false);
                GameObject.Find("ResetManager").GetComponent<ResetManager>().Reset();
                enefarmObjectManager.Target.SetActive(true);

                return;

            case TargetMode.Other:

                var others = otherObjectManager.others;
                var other = others.ToList()
                    .Where(o => o.name.Equals(this.objName))
                    .FirstOrDefault();

                if (other == null)
                {
                    Debug.LogError("TargetObjectManager-Start: objNameに一致するOtherオブジェクトが見つかりません");
                    return;
                }

                otherObjectManager.setTarget(other.mainObj,other.initialPositionZScale);
                otherObjectManager.Create();

                otherObjectManager.Target.SetActive(false);
                GameObject.Find("ResetManager").GetComponent<ResetManager>().Reset();
                otherObjectManager.Target.SetActive(true);
                return;
        }

    }

    public void Change()
    {
        if (mode == TargetMode.Enefarm)
        {
            enefarmObjectManager.ChangeTarget();
        }
    }
}
