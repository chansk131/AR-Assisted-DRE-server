using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private float speed = 1.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += Vector3.forward * 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += Vector3.back * 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += Vector3.left * 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += Vector3.right * 0.1f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
    }
}
