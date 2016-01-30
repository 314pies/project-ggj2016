using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource = null;
    public AudioClip MenuSoundEffect;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MenuSoundEffect;
        audioSource.Play();
        EventManager.StartListening( EventDictionary.ON_ENTER_GAME_STATE, OnEnterGame );
    }

    public AudioClip LoopSoundEffect;
    public AudioClip IntroSoundEffect;
    void OnEnterGame()
    {
        StartCoroutine( PlayIntroAndLoop() );
    }
    IEnumerator PlayIntroAndLoop()
    {
        audioSource.Stop();
        audioSource.PlayOneShot( IntroSoundEffect, 1.0f );
        float PlayTime = IntroSoundEffect.length;
        yield return new WaitForSeconds( PlayTime );
        audioSource.clip = LoopSoundEffect;
        audioSource.Play();
    }

    public AudioClip[] DieSoundEffect = new AudioClip[ 5 ];
    public void PlaySound_CharDead()
    {
        int Index = Random.Range( 0, DieSoundEffect.Length );
        audioSource.PlayOneShot( DieSoundEffect[ Index ], 1.0f );
    }
}