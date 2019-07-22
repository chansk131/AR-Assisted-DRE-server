using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSender : MonoBehaviour
{
    LiveStream livestream;
	// Use this for initialization
	void Start ()
    {
        livestream = GetComponent<LiveStream>();
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CustomMessages.Instance.SendForce(livestream.pressureViz);
	}
}
