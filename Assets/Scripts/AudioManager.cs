using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    private AudioSource audio=null;
    public AudioClip MenuSoundEffect;
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = MenuSoundEffect;
        audio.Play();
        EventManager.StartListening("OnEnterGameState", OnEnterGame);
    }

    public AudioClip LoopSoundEffect;
    public AudioClip IntroSoundEffect;
    void OnEnterGame()
    {
        StartCoroutine(PlayIntroAndLoop());
    }
    IEnumerator PlayIntroAndLoop()
    {
        audio.Stop();
        audio.PlayOneShot(IntroSoundEffect, 1.0f);
        float PlayTime = IntroSoundEffect.length;
        yield return new WaitForSeconds(PlayTime);
        audio.clip = LoopSoundEffect;
        audio.Play();
    }

    public AudioClip[] DieSoundEffect = new AudioClip[5];
	public void PlaySound_CharDead()
    {
        int Index = Random.Range(0, DieSoundEffect.Length);
        audio.PlayOneShot(DieSoundEffect[Index],1.0f);
    }
}
