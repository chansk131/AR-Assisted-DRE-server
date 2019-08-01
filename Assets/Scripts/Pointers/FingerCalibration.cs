using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerCalibration : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetFingerPosition()
    {
        LiveStream.Instance.TranslationOffset = Vector3.zero;
        var newOffset = (-transform.localPosition + new Vector3(-0.1f, -0.087f, -0.1f)) * 100f;
        LiveStream.Instance.TranslationOffset = new Vector3(-newOffset.z, -newOffset.x, -newOffset.y);
    }

    public void ResetFingerRotation()
    {
        LiveStream.Instance.RotationOffset = -transform.eulerAngles + new Vector3(0, 180, 0);
    }
}
