using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


public class AudioManager : MonoBehaviour {

    /* PLACE THIS CODE  WHERE YOU WANT TO THE SOUND TO OCCUR:
     * 
     * FindObjectOfType<AudioManager>().Play("ButtonClick");
     * 
     * EXAMPLE:
     * public void DamageEnemy(int damage) {
     *      health = health - damage
     *      FindObjectOfType<AudioManager>().Play("EnemyDamage");
     * 
     * }
     */


    public Sound[] sounds;
    public AudioSource audioSource;
    public static AudioManager instance;
    public void Awake() {
        instance = this;
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void SetVolume(float vol) {
        audioSource.volume = vol;
        foreach (Sound s in sounds) {
            s.source.volume = vol;
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        
    }
    public void PlaySoundAtPoint(string name,Vector3 pos)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        AudioSource.PlayClipAtPoint(s.clip, pos, s.source.volume);

    }
    public void PlayButtonClickSound() {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }


}

