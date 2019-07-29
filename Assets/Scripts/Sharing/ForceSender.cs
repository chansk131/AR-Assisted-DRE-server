using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;

public class ForceSender : Singleton<ForceSender>
{
    LiveStream livestream;

    public float Force;

	// Use this for initialization
    private void Start ()
    {
        livestream = GetComponent<LiveStream>();
		
	}
	
	// Update is called once per frame
    private void Update ()
    {
        Force = livestream.pressureViz;
        CustomMessages.Instance.SendForce(Force);
	}
}
