using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTransformSender : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //base.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        //base.SetTransformTarget();
        //// Apply transform changes, if any
        //if (Position.HasUpdate() || Rotation.HasUpdate())
        //{
        //    CustomMessages.Instance.SendTransform(CustomMessages.TestMessageID.HandTransform, transform.localPosition, transform.localRotation);
        //}
        CustomMessages.Instance.SendTransform(CustomMessages.TestMessageID.HandTransform, transform.localPosition, transform.localRotation);
    }

    //private void LateUpdate()
    //{
    //    base.UpdateTransform();
    //}

    
}
