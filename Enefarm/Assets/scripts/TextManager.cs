using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{

    private TextMesh textMesh;
    private GameObject text;
    private GameObject target;

    public TextMesh TextMesh
    {

        get
        {

            return textMesh;
        }

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (text == null && GameObject.Find("3DTextPrefab") != null)
        {
            text = GameObject.Find("3DTextPrefab");
            textMesh = GameObject.Find("3DTextPrefab").GetComponent<TextMesh>();
        }

        if (GameObject.Find("ObjectManager") == null)
        {
            return;
        }

        if (target == null)
        {
            target = GameObject.Find("ObjectManager").GetComponent<TargetObjectManager>().Target;
        }

        var position = target.transform.position;

        position.y += 0.15f;

        text.transform.position = position;

        var rotation = Camera.main.transform.rotation;

        rotation.x = text.transform.rotation.x;
        rotation.z = text.transform.rotation.z;

        text.transform.rotation = rotation;
    }
}
