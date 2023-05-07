using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpritesRenderer smallRenderer;
    public PlayerSpritesRenderer bigRenderer;
    private PlayerSpritesRenderer activeRenderer;
    private CapsuleCollider2D capsuleCollider;
    public DeathAnimation deathAnimation { get; private set; }

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starPower { get; private set; } 
    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
    }

    public void Hit()
    {
        if (!dead && !starPower)
        {
            if (big)
            {
                Shrink();
            }
            else
            {
                Death();
            }
        }
    }
    public void Shrink()
    {
        SoundManager.Instance.PlaySound(SoundManager.PlayList.marioDownToSmall);
        bigRenderer.enabled = false;
        smallRenderer.enabled = true;
        activeRenderer = smallRenderer;
        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);
        StartCoroutine(ScaleAnimation());
    }
    public void Grow()
    {
        SoundManager.Instance.PlaySound(SoundManager.PlayList.marioUpToBig);
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;
        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);
        StartCoroutine(ScaleAnimation());
    }
    public void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;
        SoundManager.Instance.PauseMusic();
        SoundManager.Instance.PlaySound(SoundManager.PlayList.marioDied);    
        GameManager.instance.ResetLvl(3f);
    }
    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !bigRenderer.enabled;
            }
            yield return null;
        }
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }
    public void Starpower(float duration = 10f)
    {
        StartCoroutine(StarPowerAnimation(duration));
    }
    private IEnumerator StarPowerAnimation(float duration)
    {
        starPower = true;
        float eplased = 0f;
        while (eplased < duration)
        {
            eplased+= Time.deltaTime;
            if(Time.frameCount % 4 == 0)
            {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            yield return null;
        }
        activeRenderer.spriteRenderer.color = Color.white;
        starPower = false;
    }
}
