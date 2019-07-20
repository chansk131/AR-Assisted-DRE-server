using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSender : MonoBehaviour
{

    protected Vector3Interpolated Position;
    protected QuaternionInterpolated Rotation;
    protected Vector3Interpolated Scale;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void Initialize()
    {
        if (Position == null)
        {
            Position = new Vector3Interpolated(transform.localPosition);
        }
        if (Rotation == null)
        {
            Rotation = new QuaternionInterpolated(transform.localRotation);
        }
        if (Scale == null)
        {
            Scale = new Vector3Interpolated(transform.localScale);
        }
    }

    protected void SetTransformTarget()
    {
        Position.SetTarget(transform.localPosition);
        Rotation.SetTarget(transform.localRotation);
    }

    protected void UpdateTransform()
    {
        // The object was moved locally, so reset the target positions to the current position
        Position.Reset(transform.localPosition);
        Rotation.Reset(transform.localRotation);
        Scale.Reset(transform.localScale);
    }
}
