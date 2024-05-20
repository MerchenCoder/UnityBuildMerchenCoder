using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimation : MonoBehaviour
{
    public Animator result_anim;
    public AnimationAudioControl animationAudioControl;

    // public AudioClip successAudioClip;
    // public AudioClip failAudioClip;

    //public Animator fail_anim;
    // Start is called before the first frame update
    void Start()
    {
        if (result_anim != null)
            animationAudioControl = result_anim.GetComponent<AnimationAudioControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Success()
    {
        if (result_anim == null)
        {
            Debug.Log("null break");
            yield break;

        }

        result_anim.SetInteger("Control", 10);
        yield return new WaitForSeconds(2.5f);
        result_anim.SetInteger("Control", 0);
        animationAudioControl.StopAnimationSound();
        yield return null;

    }
    public IEnumerator Fail()
    {
        if (result_anim == null)
        {
            Debug.Log("null break");
            yield break;
        }

        result_anim.SetInteger("Control", -10);
        yield return new WaitForSeconds(2.5f);
        result_anim.SetInteger("Control", 0);
        animationAudioControl.StopAnimationSound();
        yield return null;
    }
}
