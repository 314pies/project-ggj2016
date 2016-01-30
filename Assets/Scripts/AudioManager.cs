using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }


    private AudioSource audio;
    public AudioClip[] DieSoundEffect = new AudioClip[5];
	public void PlaySound_CharDead()
    {
        int Index = Random.Range(0, DieSoundEffect.Length);
        audio.PlayOneShot(DieSoundEffect[Index],1.0f);
    }
}
