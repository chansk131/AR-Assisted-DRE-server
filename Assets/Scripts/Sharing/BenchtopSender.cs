using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchtopSender : MonoBehaviour
{

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        CustomMessages.Instance.SendTransform(CustomMessages.TestMessageID.BenchtopTransform, transform.localPosition, transform.localRotation);
    }
}
