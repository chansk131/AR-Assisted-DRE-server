using UnityEngine;

public class FollowObject : MonoBehaviour
{

    public GameObject player;


    private Vector3 offset;

    // Use this for initialization
    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}