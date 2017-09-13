using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class EnefarmTapEvent : MonoBehaviour,IInputClickHandler {

    private TargetObjectManager targetObjectManager;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        targetObjectManager.ChangeTarget();
    }

    private void OnEnable()
    {
        targetObjectManager = GameObject.Find("ObjectManager").GetComponent<TargetObjectManager>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
