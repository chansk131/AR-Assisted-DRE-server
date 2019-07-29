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

    public void ResetFingerRotation()
    {
        LiveStream.Instance.RotationOffset = -transform.eulerAngles + new Vector3(0, 180, 0);
    }
}
