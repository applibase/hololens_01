using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectTapEvent : MonoBehaviour, IInputClickHandler
{
    private SelectEvent select;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        select.Select();
    }

    private void OnEnable()
    {
        select = GameObject.Find("MenuManager").GetComponent<SelectEvent>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
