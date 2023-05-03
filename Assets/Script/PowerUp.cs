using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        StarPower,
    }
    public Type type;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }
    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.instance.AddCoin();
                break;
            case Type.ExtraLife:
                GameManager.instance.AddLife();
                break;
            case Type.StarPower:
                break;
            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
        }
        Destroy(gameObject);
    }
}
