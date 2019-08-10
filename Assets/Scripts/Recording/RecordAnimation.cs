using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Animations;
using UnityEngine;

public class RecordAnimation : MonoBehaviour
{
    public Transform Benchtop;
    public AnimationClip clip;
    public bool record = false;

    private GameObjectRecorder m_Recorder;

    void Start()
    {
        m_Recorder = new GameObjectRecorder();
        m_Recorder.root = gameObject;

        m_Recorder.BindComponent<Transform>(gameObject, true);
    }

    void LateUpdate()
    {
        if (clip == null)
            return;

        if (record)
        {
            PlayprefPositionHelper.SaveBenchtopTransform(Benchtop.localPosition, Benchtop.localRotation);
            m_Recorder.TakeSnapshot(Time.deltaTime);
        }
        else if (m_Recorder.isRecording)
        {
            m_Recorder.SaveToClip(clip);
            m_Recorder.ResetRecording();
        }
    }
}
