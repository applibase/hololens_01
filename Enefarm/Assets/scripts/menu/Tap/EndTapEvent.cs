using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndTapEvent : MonoBehaviour,IInputClickHandler
{

    private EndEvent end;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        end.End();
    }

    private void OnEnable()
    {
        end = GameObject.Find("MenuManager").GetComponent<EndEvent>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   
}
