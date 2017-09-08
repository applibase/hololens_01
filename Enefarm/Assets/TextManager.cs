using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour {

    public TextMesh textMesh;
    private GameObject text;
    private GameObject target;

	// Use this for initialization
	void Start () {
  
	}
	
	// Update is called once per frame
	void Update () {

        if (text == null &&  GameObject.Find("3DTextPrefab") != null)
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

	}
}
