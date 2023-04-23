using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpritesRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMoving playerMoving;
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprites run;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMoving = GetComponentInParent<PlayerMoving>();
    }
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }
    private void LateUpdate()
    {
        run.enabled = playerMoving.running;  
        if (playerMoving.jumping)
        {
            spriteRenderer.sprite = jump;
        }else if (playerMoving.sliding)
        {
            spriteRenderer.sprite = slide;
        }else if (!playerMoving.running)
        {
            spriteRenderer.sprite = idle; 
        }
    }
}
