using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }
    public Text textCoins;
    public Text textLives;
    public Text textGameOver;
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
        Application.targetFrameRate = 60;
        NewGame();
    }
    private void NewGame()
    {
        textGameOver.enabled = false;
        SoundManager.Instance.PlayMusic(SoundManager.PlayList.music);
        lives = 3;
     //   SetLives(3);
        coins = 0;
     //   SetCoin(0);
        LoadLvl(1, 1);
    }
    public void LoadLvl(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
        SoundManager.Instance.ContinueMusic(SoundManager.PlayList.music);
    }
    public void ResetLvl(float delay)
    {
        Invoke(nameof(ResetLvl), delay);
    }
    public void ResetLvl()
    {
        lives--;
     //   SetLives(lives);
        if (lives > 0)
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
        textGameOver.enabled = true;
        SoundManager.Instance.PlaySound(SoundManager.PlayList.gameOver);
        NewGame();
    }
    public void AddCoin()
    {
        SoundManager.Instance.PlaySound(SoundManager.PlayList.marioPickCoin);
        coins++;
     //   SetCoin(coins);
        if(coins == 100)
        {
            AddLife();
            coins = 0;
         //   SetCoin(coins);
        }
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
        textLives.text = "x" + lives.ToString();

    }
    private void SetCoin(int coins)
    {
        this.coins = coins;
        textCoins.text = coins.ToString().PadLeft(2, '0');
    }
    public void AddLife()
    {
        lives++;
       // SetLives(lives);
    }
}
