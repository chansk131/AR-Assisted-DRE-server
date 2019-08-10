using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceVisualiser : MonoBehaviour
{

    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private InputField forceText;
    [SerializeField]
    private float forceMax = 10;

    private float force;

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        // Send force to client
        force = ForceSender.Instance.Force;

        // Set force text
        forceText.text = "Force: " + force.ToString("F2") + " N";

        // Set force bar
        foregroundImage.fillAmount = force / forceMax;
    }
}