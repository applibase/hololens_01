using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hide()
    {
        var menu = GameObject.Find("Menu");

        if (menu != null && menu.activeSelf)
        {
            menu.SetActive(false);
        }
    }
}
