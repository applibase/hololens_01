using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResetTapEvent : MonoBehaviour, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        //Debug.Log("Resetボタンクリック");
        Reset();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        var ResetManager = GameObject.Find("ResetManager");

        ResetManager.GetComponent<ResetManager>().Reset();
    }
}
