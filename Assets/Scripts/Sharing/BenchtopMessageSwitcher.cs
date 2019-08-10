using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchtopMessageSwitcher : MonoBehaviour
{
    [SerializeField]
    private bool isTransmitting = false;
    [SerializeField]
    private BenchtopSender sender;
    [SerializeField]
    private BenchtopTransformReceiver receiver;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetTransmitStatus(bool transmit)
    {
        receiver.enabled = !transmit;
        sender.enabled = transmit;
    }

    public void ToggleTransmitBenchtop()
    {
        isTransmitting = !isTransmitting;
        SetTransmitStatus(isTransmitting);
    }
}
