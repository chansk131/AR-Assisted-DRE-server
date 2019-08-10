using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayAnimation : MonoBehaviour
{
    [SerializeField]
    private bool replay = false;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform benchtop;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleAnimator()
    {
        replay = !replay;
        animator.enabled = replay;

        if (replay)
        {
            benchtop.localPosition = PlayprefPositionHelper.LoadBenchtopPosition();
            benchtop.localRotation = PlayprefPositionHelper.LoadBenchtopRotation();
            animator.Play("ReplayAnimation", -1, 0f);
        }
    }
}
