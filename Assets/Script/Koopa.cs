using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;
    private bool shelled;
    private bool pushed;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.starPower)
            {
                Hit();
            }
            else if (collision.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {
                player.Hit();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (shelled && other.CompareTag("Player"))
        {
            if (!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();
                if (player.starPower)
                {
                    Hit();
                }
                else
                {
                    player.Hit();
                } 
            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }
    private void PushShell(Vector2 direction)
    {
        pushed = true;
        SoundManager.Instance.PlaySound(SoundManager.PlayList.marioKickShell);
        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }
    private void EnterShell()
    {
        SoundManager.Instance.PlaySound(SoundManager.PlayList.marioStomp);
        shelled = true;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprites>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }
    private void OnBecameInvisible()
    {
        if (pushed)
        {
            Destroy(gameObject);
        }
    }
    private void Hit()
    {
        GetComponent<AnimatedSprites>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
