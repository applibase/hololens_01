using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdjustTapEvent : MonoBehaviour, IInputClickHandler
{

    private AdJustEvent adjust;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

	}

    private void OnEnable()
    {
        adjust = GameObject.Find("MenuManager").GetComponent<AdJustEvent>();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        adjust.Adjust();
    }


}
