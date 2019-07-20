using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchtopTransformReceiver : MonoBehaviour
{

    TransformProcessor processor;

    void Start()
    {
        processor = GetComponent<TransformProcessor>();
        CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.BenchtopTransform] = processor.ProcessTransform;
    }
}
