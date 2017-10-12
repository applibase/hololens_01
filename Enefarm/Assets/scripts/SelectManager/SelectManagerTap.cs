using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectManagerTap : MonoBehaviour, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {

        var gameObject = GazeManager.Instance.HitObject;

        if (gameObject == null)
        {
            return;
        }

        if (gameObject.tag.Equals("selectObject"))
        {

            if (gameObject.transform.root.gameObject != null)
            {
                var parent = gameObject.transform.parent.gameObject;

                try
                {
                    int h = Int32.Parse(parent.name);
                    
                    GameObject.Find("MenuManager").GetComponent<SelectEvent>().Create(h);
                    GameObject.Find("MenuManager").GetComponent<SelectEvent>().ParentObj.SetActive(false);
                }
                catch (FormatException e)
                {
                    int h = Int32.Parse(gameObject.name);
                    //GameObject.Find("MenuManager").GetComponent<SelectEvent>().DeleteTargetObj();
                    GameObject.Find("MenuManager").GetComponent<SelectEvent>().Create(h);
                    GameObject.Find("MenuManager").GetComponent<SelectEvent>().ParentObj.SetActive(false);
                }

            }


        }
    }

    // Use this for initialization
    void Start()
    {
        InputManager.Instance.AddGlobalListener(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
