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
        lives = 3;
        LoadLvl(1, 1);
    }
    private void LoadLvl(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
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

}
