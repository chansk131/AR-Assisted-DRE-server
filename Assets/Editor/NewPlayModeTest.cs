using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NewPlayModeTest {

	[Test]
	public void NewPlayModeTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator NewPlayModeTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
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
