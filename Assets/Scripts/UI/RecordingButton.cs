using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordingButton : MonoBehaviour
{

    [SerializeField]
    private bool isClicked = false;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text text;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IsRecordingButton(bool isButtonClicked)
    {
        isClicked = isButtonClicked;
        ChangeButtonColour(isClicked);
    }

    public void ToggleButton()
    {
        isClicked = !isClicked;
        IsRecordingButton(isClicked);

    }

    private void ChangeButtonColour(bool isButtonClicked)
    {
        if (isButtonClicked)
        {
            image.color = Color.red;
            text.text = "Stop Recording";
            text.color = Color.white;
        }
        else
        {
            image.color = Color.green;
            text.text = "Start Recording";
            text.color = Color.black;
        }
    }
}
