using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordDRE : MonoBehaviour
{
    public bool IsRecording = false;
    public RecordAnimation recordAnimation;
    private RecordingButton recordingButton;

    // Use this for initialization
    void Start()
    {
        recordingButton = GetComponent<RecordingButton>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleRecord()
    {
        IsRecording = !IsRecording;

        // set button appearance
        recordingButton.IsRecordingButton(IsRecording);

        // start record the finger movement
        recordAnimation.record = IsRecording;
    }
}
