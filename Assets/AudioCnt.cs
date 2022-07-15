using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCnt : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audios;

    private AudioSource audioScr;

    private void Start()
    {
        audioScr = GetComponent<AudioSource>();
    }

    public void ballPushed()
    {
        audioScr.Pause();
        audioScr.clip = audios[0];
        audioScr.Play();
    }

    public void looseGame()
    {
        audioScr.Pause();
        audioScr.clip = audios[4];
        audioScr.Play();
    }

    public void buttonClick()
    {
        audioScr.Pause();
        audioScr.clip = audios[1];
        audioScr.Play();
    }

    public void scoreChange()
    {
        audioScr.Pause();
        audioScr.clip = audios[2];
        audioScr.Play();
    }

    public void winGame()
    {
        audioScr.Pause();
        audioScr.clip = audios[3];
        audioScr.Play();
    }
}
