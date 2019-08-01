using HoloToolkit.Sharing;
using UnityEngine;
using UnityEngine.UI;

public class ProstateReceiver : MonoBehaviour
{
    [SerializeField]
    private Dropdown prostateDropdown;
    [SerializeField]
    private CustomMessages.TestMessageID testMessageId = CustomMessages.TestMessageID.Prostate;

    private void Start()
    {
        CustomMessages.Instance.MessageHandlers[testMessageId] = ProcessProstate;
    }

    public void ProcessProstate(NetworkInMessage msg)
    {
        long userID = msg.ReadInt64();
        var prostate = CustomMessages.Instance.ReadInt(msg);          // change prostate state         Game.Instance.Prostate = (ProstateType)prostate;

        // change prostate menu
        prostateDropdown.value = prostate;
    }
}
