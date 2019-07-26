using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] attackAudios;


    public void AudioSlash()
    {
        source.clip = attackAudios[0];
        source.Play();
    }

    public void AudioStomp()
    {
        source.clip = attackAudios[1];
        source.Play();
    }

    public void AudioHurt()
    {
        source.clip = attackAudios[2];
        source.Play();
    }

    public void AudioDeath()
    {
        source.clip = attackAudios[3];
        source.Play();
    }

    public void AudioDash()
    {
        source.clip = attackAudios[4];
        source.Play();
    }

    public void AudioBlock()
    {
        source.clip = attackAudios[5];
        source.Play();
    }

    public void AudioHeal()
    {
        source.clip = attackAudios[6];
        source.Play();
    }

    public void AudioHealEnd()
    {
        source.clip = attackAudios[7];
        source.Play();
    }

    public void AudioDoge()
    {
        source.clip = attackAudios[8];
        source.Play();
    }

    public void AudioStep()
    {
        int i = Convert.ToInt32
            (Random.Range(9, 13));
        source.clip = attackAudios[i];
        source.Play();
    }

}
