using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G_UI : MonoBehaviour
{
    public Text textCoins;
    public Text textLives;
    public Text textGameOver;

    private void Awake()
    { 
    }
    private void Start()
    {
        GameManager.instance.ui = this;
        textCoins.text = GameManager.instance.coins.ToString();
        textLives.text = GameManager.instance.lives.ToString();
        
    }
}
