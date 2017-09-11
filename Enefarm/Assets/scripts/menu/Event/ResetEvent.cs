using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEvent : MonoBehaviour {

    private ResetManager resetManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        resetManager = GameObject.Find("ResetManager").GetComponent<ResetManager>();
    }
    public void Reset()
    {
        resetManager.Reset();
    }
}
