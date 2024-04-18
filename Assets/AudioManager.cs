using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;

    public AudioClip Shooting;
    public AudioClip background;
    public AudioClip steps;
    public AudioClip hitplayer;
    public AudioClip alarm;
    public AudioClip ZombieBoss;

    public void PlaySFX(AudioClip clip){

        SFXSource.PlayOneShot(clip);
    }

    private void Start(){

        PlaySFX(background);
    }


}
