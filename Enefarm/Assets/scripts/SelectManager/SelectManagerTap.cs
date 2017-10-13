using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectManagerTap : MonoBehaviour, IInputClickHandler
{

    private SelectEvent selectEvent;

    public void OnInputClicked(InputClickedEventData eventData)
    {

        var gameObject = GazeManager.Instance.HitObject;

        if (gameObject == null)
        {
            return;
        }

        if (gameObject.tag.Equals("selectObject") && gameObject.transform.root.gameObject != null)
        {

            var parent = gameObject.transform.parent.gameObject;

            if (CheckInt(parent.name))
            {
                selectEvent.CreateTargetObj(Int32.Parse(parent.name));
                selectEvent.ParentObj.SetActive(false);
                return;
            }

            selectEvent.CreateTargetObj(Int32.Parse(gameObject.name));
            selectEvent.ParentObj.SetActive(false);

        }
    }

    private bool CheckInt(String str)
    {
        try
        {
            Int32.Parse(str);
            return true;
        }
        catch 
        {
            return false;
        }
    }
    // Use this for initialization
    void Start()
    {
        InputManager.Instance.AddGlobalListener(gameObject);
        selectEvent = GameObject.Find("MenuManager").GetComponent<SelectEvent>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
