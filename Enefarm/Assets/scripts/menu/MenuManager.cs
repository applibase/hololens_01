using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.SpatialMapping;

public class MenuManager : MonoBehaviour, IInputClickHandler
{

    public GameObject menu;

    public float menuPositionScale;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        var gameObject = GazeManager.Instance.HitObject;

        if (gameObject == null || gameObject.name.Contains("Surface"))
        {
            activeMenu();
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
            var rotation = Camera.main.transform.rotation;

            rotation.x = menu.transform.rotation.x;
            rotation.z = menu.transform.rotation.z;

            menu.transform.rotation = rotation;

            var pos = Camera.main.transform.position;

            var forward = Camera.main.transform.forward;

            if (menuPositionScale != 0f)
            {
                forward = Camera.main.transform.forward * menuPositionScale;
            }
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
