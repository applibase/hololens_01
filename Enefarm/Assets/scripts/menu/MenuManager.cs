using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.SpatialMapping;

public class MenuManager : MonoBehaviour, IInputClickHandler
{

    public GameObject menu;
    private RaycastHit hitInfo;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        var gameObject = GazeManager.Instance.HitObject;

        if (gameObject == null)
        {
            activeMenu();
        }
        
    }

    // Use this for initialization
    void Start ()
    {
        InputManager.Instance.AddGlobalListener(gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        if (!menu.activeSelf)
        {
            return;
        }

        var cursor = GameObject.Find("DefaultCursor");

        Vector3 menuPos = menu.transform.position;
        Vector3 cursorPos = cursor.transform.position;
        float dis = Vector3.Distance(menuPos, cursorPos);

        if (dis >= 2.0f)
        {
            float angle = 0.0F;
            Vector3 axis = Vector3.zero;

            Camera.main.transform.rotation.ToAngleAxis(out angle, out axis);
            menu.transform.rotation = Quaternion.AngleAxis(angle, axis);

            var pos = Camera.main.transform.position;
            var forward = Camera.main.transform.forward;

            menu.transform.position = pos + forward;
        }
    }
    
    public void activeMenu()
    {
        if (!menu.activeSelf)
        {
            menu.SetActive(true);
        }
    }



}
