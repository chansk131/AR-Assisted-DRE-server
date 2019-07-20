using HoloToolkit.Sharing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformProcessor : MonoBehaviour {

    private Vector3 _position;
    public Vector3 Position
    {
        get
        {
            return _position;
        }

        private set
        {
            _position = value;
            if (_position != transform.localPosition)
                this.transform.localPosition = _position;
        }
    }

    private Quaternion _rotation;
    public Quaternion Rotation
    {
        get
        {
            return _rotation;
        }

        private set
        {
            _rotation = value;
            this.transform.localRotation = _rotation;
        }
    }

    private void Start()
    {

    }

    public void ProcessTransform(NetworkInMessage msg)
    {
        long userID = msg.ReadInt64();
        Position = CustomMessages.Instance.ReadVector3(msg);
        Rotation = CustomMessages.Instance.ReadQuaternion(msg);
    }
}
