using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEvent : MonoBehaviour
{

    public const float selectScale = 0.05f;
    public const float selectSpace = 0.1f;

    private List<GameObject> mainObjList = new List<GameObject>();
    private List<GameObject> selectObjList = new List<GameObject>();

    private GameObject parentObj;
    private HideEvent hideEvent;

    private Other[] others;
    private Enefarm[] enefarms;

    public GameObject ParentObj
    {
        get
        {
            return parentObj;
        }
    }



    // Use this for initialization


    void Start()
    {
        hideEvent = GameObject.Find("MenuManager").GetComponent<HideEvent>();
        Debug.Log("Select");

        getChosenObjPrefab();
        CreateSelectParent();

        var pos = Camera.main.transform.position + (Camera.main.transform.forward);

        for (int h = 0; h < mainObjList.Count; h++)
        {
            GameObject chosenObj = Instantiate(mainObjList[h], pos, new Quaternion());
            SetScale(chosenObj);
            SetNameAndTag(chosenObj, h);

            pos.x = pos.x + chosenObj.transform.lossyScale.x / 2f;
            pos.x = pos.x + selectSpace;

            var rigidbody = chosenObj.GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbody.isKinematic = true;

            chosenObj.transform.parent = parentObj.transform;
            selectObjList.Add(chosenObj);
        }

        parentObj.SetActive(false);
        SortObj();
    }

    public void Select()
    {
        parentObj.SetActive(true);
        var pos = Camera.main.transform.position + (Camera.main.transform.forward);
        parentObj.transform.position = pos;
        parentObj.transform.rotation = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0);

        hideEvent.Hide();

    }

    private void CreateSelectParent()
    {
        var pos = Camera.main.transform.position + (Camera.main.transform.forward);
        var parentObjPrefab = new GameObject("SelectParent");
        parentObj = Instantiate(parentObjPrefab, pos, new Quaternion());
        parentObj.transform.rotation = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0);
        Destroy(parentObjPrefab, 0f);
    }

    private void getChosenObjPrefab()
    {
        var otherObjectManager = GameObject.Find("ObjectManager").GetComponent<OtherObjectManager>();
        others = otherObjectManager.others;

        var enefarmObjectManager = GameObject.Find("ObjectManager").GetComponent<EnefarmObjectManager>();
        enefarms = enefarmObjectManager.enefarms;

        foreach (var enefarm in enefarms)
        {
            var mainObj = enefarm.mainObj;
            mainObjList.Add(mainObj);
        }

        foreach (var other in others)
        {
            var mainObj = other.mainObj;
            mainObjList.Add(mainObj);
        }
    }

    private void SortObj()
    {
        var x = selectObjList[selectObjList.Count - 1].transform.position.x / 2f;
        for(int i = 0;  i < selectObjList.Count; i++)
        {
            var pos = selectObjList[i].transform.position;
            pos.x = selectObjList[i].transform.position.x - x;

            selectObjList[i].transform.position = pos;
        }

    }



    private void SetNameAndTag(GameObject gObj, int h)
    {
        gObj.name = "" + h;
        gObj.tag = "selectObject";

        for (int j = 0; j < gObj.transform.childCount; j++)
        {
            gObj.transform.GetChild(j).tag = "selectObject";
        }
    }

    private void SetScale(GameObject gObj)
    {
        float high = gObj.transform.localScale.y;
        float scale = selectScale / high;

        var x = gObj.transform.localScale.x;
        var y = gObj.transform.localScale.y;
        var z = gObj.transform.localScale.z;

        var newScale = new Vector3(x * scale, y * scale, z * scale);

        gObj.transform.localScale = newScale;
    }

    public void Create(int h)
    {
        var targetName = SelectCreateObj(h);
        var targetObjectManager = GameObject.Find("ObjectManager").GetComponent<TargetObjectManager>();

        targetObjectManager.objName = targetName;

        targetObjectManager.Create();

    }

    private void ClearList()
    {
        this.mainObjList.Clear();
        this.selectObjList.Clear();
    }

    private string SelectCreateObj(int h)
    {
        var obj = mainObjList[h];
        var targetObjectManager = GameObject.Find("ObjectManager").GetComponent<TargetObjectManager>();

        foreach (var other in others)
        {
            if (other.mainObj.name == obj.name)
            {
                targetObjectManager.mode = TargetMode.Other;
                return other.name;
            }

        }

        foreach (var enefarm in enefarms)
        {
            if (enefarm.mainObj.name == obj.name)
            {
                targetObjectManager.mode = TargetMode.Enefarm;
                return enefarm.name;
            }

        }


        return null;
    }

    public void DeleteTargetObj()
    {
        var dTargetObj = GameObject.Find("ObjectManager").GetComponent<TargetObjectManager>().Target;
        Destroy(dTargetObj, 0f);
    }
}
