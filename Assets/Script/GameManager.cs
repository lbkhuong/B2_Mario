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
    public TextMeshProUGUI textCoins;
    public TextMeshProUGUI textLives;
    public TextMeshProUGUI textGameOver;
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
        //textGameOver.enabled = false;
        SoundManager.Instance.PlayMusic(SoundManager.PlayList.music);
        lives = 3;
        coins = 0;
        //textLives.text = "x3"; 
        //textCoins.text = "00";
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
       // textLives.SetText("x" + lives.ToString());
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
        //textGameOver.enabled = true;
        //SoundManager.Instance.PlaySound(SoundManager.PlayList.gameOver);
        NewGame();
    }
    public void AddCoin()
    {
        SoundManager.Instance.PlaySound(SoundManager.PlayList.marioPickCoin);
        coins++;
        //textCoins.SetText(coins.ToString());
        if(coins == 100)
        {
            AddLife();
            coins = 0;
           // textCoins.SetText("00");
        }
    }
    public void AddLife()
    {
        lives++;
        //textLives.SetText("x" + lives.ToString());
    }
}
