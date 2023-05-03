using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Animate());
    }
    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody.isKinematic = true;
        circleCollider.enabled = false;
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.enabled = true;
        float elapsed = 0f;
        float duration = 0.5f;
        Vector3 startPostion = transform.localPosition;
        Vector3 endPostion = transform.localPosition + Vector3.up;
        while(elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPostion, endPostion, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPostion;
        rigidbody.isKinematic = false;
        boxCollider.enabled = true;
        circleCollider.enabled = true;
    }
}
