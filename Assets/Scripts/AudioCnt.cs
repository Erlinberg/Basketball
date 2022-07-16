using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCnt : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audios;

    private AudioSource audioScr;

    [HideInInspector]
    public bool audioEnabled = true;

    private void Start()
    {
        audioScr = GetComponent<AudioSource>();
    }

    public void ballPushed()
    {
        if (!audioEnabled) { return; }
        audioScr.Pause();
        audioScr.clip = audios[0];
        audioScr.Play();
    }

    public void looseGame()
    {
        if (!audioEnabled) { return; }
        audioScr.Pause();
        audioScr.clip = audios[4];
        audioScr.Play();
    }

    public void buttonClick()
    {
        if (!audioEnabled) { return; }
        audioScr.Pause();
        audioScr.clip = audios[1];
        audioScr.Play();
    }

    public void scoreChange()
    {
        if (!audioEnabled) { return; }
        audioScr.Pause();
        audioScr.clip = audios[2];
        audioScr.Play();
    }

    public void winGame()
    {
        if (!audioEnabled) { return; }
        audioScr.Pause();
        audioScr.clip = audios[3];
        audioScr.Play();
    }
}
