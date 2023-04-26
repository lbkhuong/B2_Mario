using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpritesRenderer smallRenderer;
    public PlayerSpritesRenderer bigRenderer;
    public DeathAnimation deathAnimation { get; private set; }

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }

    public void Hit()
    {
        if (!dead)
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
       
    }

    public void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;
        SoundManager.Instance.PlaySound(SoundManager.PlayList.marioDied);
        GameManager.instance.ResetLvl(3f);
    }
}
