using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public KeyCode enterKeycode = KeyCode.S;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;
    public Transform connection;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (connection!=null && collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(enterKeycode))
            {
                StartCoroutine(Enter(collision.transform));
            }
        }
    }

    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMoving>().enabled = false;
        Vector3 enteredPosition = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;
        yield return Move(player, enteredPosition, enteredScale);
        yield return new WaitForSeconds(1f);
        bool underWorld = connection.position.y < 0f;
        
        Camera.main.GetComponent<SideScrolling>().SetUnderWorld(underWorld);
        if( exitDirection != Vector3.zero)
        {   
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
        player.GetComponent<PlayerMoving>().enabled = true;
        player.GetComponent<Player>().inUnderGround(underWorld);
    }

    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        SoundManager.Instance.PlaySound(SoundManager.PlayList.pipe);
        
        float eplased = 0f;
        float duration = 1f;
        Vector3 startPosition = player.position;
        Vector3 startScale = player.lossyScale;
        while(eplased < duration)
        {
            float t = eplased / duration;
            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            eplased += Time.deltaTime;
            yield return null;
        }
        player.position = endPosition;
        player.localScale = endScale;
    }
}
