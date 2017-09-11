using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResetTapEvent : MonoBehaviour, IInputClickHandler
{

    private ResetEvent reset;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        reset.Reset();

    }

    private void OnEnable()
    {
        reset = GameObject.Find("MenuManager").GetComponent<ResetEvent>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
