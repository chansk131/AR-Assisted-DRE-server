using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NewEditModeTest
{

	[Test]
	public void NewEditModeTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

    [Test]
    public void NotMoveBenchtopNotMoveCamera()
    {
        var benchtop = new GameObject();
        benchtop.transform.position = Vector3.zero;

        var camera = new GameObject();
        var initialTransform = new Vector3(0.5f, 0, -1.6f);
        camera.transform.position = initialTransform;

        camera.AddComponent<FollowObject>();

        Assert.AreEqual(initialTransform, camera.transform.position);
    }

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator NewEditModeTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
	}
}
