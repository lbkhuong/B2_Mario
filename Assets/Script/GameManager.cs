using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }
    private void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
    private void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        SoundManager.Instance.PlayMusic(SoundManager.PlayList.music);
        lives = 3;
        coins = 0;
        LoadLvl(1, 1);
    }
    private void LoadLvl(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
        SoundManager.Instance.ContinueMusic();
    }
    public void ResetLvl(float delay)
    {
        Invoke(nameof(ResetLvl), delay);
    }
    public void ResetLvl()
    {
        lives--;
        if(lives > 0)
        {
            LoadLvl(world, stage);
        }
        else
        {
            GameOver();
        }
    }
    public void NextLvl()
    {
        LoadLvl(world, stage+1);
    }
    public void GameOver()
    {
        NewGame();
    }
    public void AddCoin()
    {
        SoundManager.Instance.PlaySound(SoundManager.PlayList.marioPickCoin);
        coins++;
        if(coins == 100)
        {
            AddLife();
            coins = 0;
        }
    }
    public void AddLife()
    {
        lives++;
    }
}
