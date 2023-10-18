using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSwitch : MonoBehaviour
{
    [SerializeField] VerticalMoveableGround moveableGround;
    Animator myAnimator;
    AudioSource myAudioSource;

    bool isOn = false;

    public bool IsOn => isOn;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }

    public void SwitchOn()
    {
        isOn = true;

        myAnimator.SetBool("isOn", true);

        myAudioSource.Play();

        moveableGround.Go();
    }

    public void SwitchOff()
    {
        isOn = false;

        myAnimator.SetBool("isOn", false);

        myAudioSource.Play();

        moveableGround.Return();
    }
}
