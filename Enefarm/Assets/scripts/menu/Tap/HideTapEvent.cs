using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HideTapEvent : MonoBehaviour, IInputClickHandler
{

    private HideEvent hide;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        hide.Hide();
    }

    private void OnEnable()
    {
        hide = GameObject.Find("MenuManager").GetComponent<HideEvent>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
