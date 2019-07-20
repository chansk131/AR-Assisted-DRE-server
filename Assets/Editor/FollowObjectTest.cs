using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class FollowObjectTest
{

    [Test]
    public void NotMoveBenchtopNotMoveCamera()
    {
        var benchtop = new GameObject();
        benchtop.transform.position = Vector3.zero;

        var camera = new GameObject();
        var initialTransform = new Vector3(0.5f, 0, -1.6f);
        camera.transform.position = initialTransform;

        camera.AddComponent<FollowObject>();
        var followObject = camera.GetComponent<FollowObject>();
        followObject.player = benchtop;

        Assert.AreEqual(initialTransform, camera.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveForwardBenchtopMoveForwardCamera()
    {
        var benchtop = new GameObject();
        benchtop.transform.position = Vector3.zero;

        var camera = new GameObject();
        var initialTransform = new Vector3(0.5f, 0, -1.6f);
        camera.transform.position = initialTransform;

        camera.AddComponent<FollowObject>();
        var followObject = camera.GetComponent<FollowObject>();
        followObject.player = benchtop;

        var newTransform = new Vector3(1, 0, 0);
        benchtop.transform.position = newTransform;

        yield return new WaitForFixedUpdate();


        Assert.AreEqual(initialTransform + newTransform, camera.transform.position);
    }
}
