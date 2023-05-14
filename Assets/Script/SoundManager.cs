using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public enum PlayList
    {
        downTheFlag = 0,
        gameOver = 1,
        marioJump = 2,
        marioUpToBig = 3,
        marioDownToSmall = 4,
        marioDied = 5,
        marioPickCoin = 6,
        marioKickShell = 7,
        music = 8,
        marioStomp = 9,
        shellBump = 10,
        pipe = 11,
        stageClear = 12,
        marioPowerUp = 13,
        marioInUnderGround = 14,
    }
    public List<AudioClip> audioLists;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
    public void PlaySound(PlayList soundType)
    {
        effectsSource.PlayOneShot(audioLists[(int)soundType]);
    }

    public void PlayMusic(PlayList soundType)
    {
        musicSource.clip = audioLists[(int)soundType];
        musicSource.Play();
    }

    public void StopMusic(PlayList soundType)
    {
        musicSource.clip = audioLists[(int)soundType];
        musicSource.Stop();
    }

    public void PauseMusic(PlayList soundType)
    {
        musicSource.clip = audioLists[(int)soundType];
        musicSource.Pause();
    }

    public void ContinueMusic(PlayList soundType)
    {
        musicSource.clip = audioLists[(int)soundType];
        musicSource.Play();
    }

    public void Audio()
    {

    }
}
