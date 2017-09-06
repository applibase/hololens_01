using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var pos = Camera.main.transform.position;
        var forward = Camera.main.transform.forward;

        var menu = GameObject.Find("Menu");
        var cursor = GameObject.Find("DefaultCursor");

        Vector3 menuPos = menu.transform.position;
        Vector3 cursorPos = cursor.transform.position;
        float dis = Vector3.Distance(menuPos, cursorPos);

        if (dis >= 2.0f)
        {
            float angle = 0.0F;
            Vector3 axis = Vector3.zero;
            menu.transform.position = pos + forward;
            Camera.main.transform.rotation.ToAngleAxis(out angle, out axis);
            menu.transform.rotation = Quaternion.AngleAxis(angle, axis);
        }

    }
}
