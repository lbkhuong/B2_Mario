using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;
    //public int nextWorld = 2;
    //public int nextStage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(SoundManager.PlayList.downTheFlag);
            StartCoroutine(MoveTo(flag, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(other.transform));
        }
    }
    private IEnumerator MoveTo( Transform subject, Vector3 destination)
    {
        while(Vector3.Distance(subject.position, destination) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            yield return null;
        }
        subject.position = destination;
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<PlayerMoving>().enabled = false;
        yield return MoveTo(player, poleBottom.position);
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.left + Vector3.down);
        yield return MoveTo(player, castle.position);
        SoundManager.Instance.PauseMusic(SoundManager.PlayList.music);
        SoundManager.Instance.PlaySound(SoundManager.PlayList.stageClear);
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        //GameManager.instance.LoadLvl(nextWorld, nextStage);
    }
}
