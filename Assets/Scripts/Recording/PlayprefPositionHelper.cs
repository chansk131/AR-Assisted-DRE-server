using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayprefPositionHelper
{
    public static void SaveBenchtopTransform(Vector3 position, Quaternion rotation)
    {
        PlayerPrefs.SetFloat("positionX", position.x);
        PlayerPrefs.SetFloat("positionY", position.y);
        PlayerPrefs.SetFloat("positionZ", position.z);

        PlayerPrefs.SetFloat("rotationX", rotation.x);
        PlayerPrefs.SetFloat("rotationY", rotation.y);
        PlayerPrefs.SetFloat("rotationZ", rotation.z);
        PlayerPrefs.SetFloat("rotationW", rotation.w);
    }

    public static Vector3 LoadBenchtopPosition()
    {
        if (!HasSave())
        {
            Debug.Log("No save");
            return Vector3.zero;
        }

        var positionX = PlayerPrefs.GetFloat("positionX");
        var positionY = PlayerPrefs.GetFloat("positionY");
        var positionZ = PlayerPrefs.GetFloat("positionZ");

        return new Vector3(positionX, positionY, positionZ);
    }

    public static Quaternion LoadBenchtopRotation()
    {
        if (!HasSave())
        {
            Debug.Log("No save");
            return Quaternion.identity;
        }

        var rotationX = PlayerPrefs.GetFloat("rotationX");
        var rotationY = PlayerPrefs.GetFloat("rotationY");
        var rotationZ = PlayerPrefs.GetFloat("rotationZ");
        var rotationW = PlayerPrefs.GetFloat("rotationW");

        return new Quaternion(rotationX, rotationY, rotationZ, rotationW);
    }

    public static bool HasSave()
    {
        return PlayerPrefs.HasKey("positionX");
    }
}
