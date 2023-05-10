using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    public float heigh = 6.5f;
    public float underWorldHeigh = -9.5f;
    private Transform player;
    private Vector3 cameraPosition;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }
    public void SetUnderWorld(bool underWorld)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underWorld ? underWorldHeigh : heigh;
        transform.position = cameraPosition;
    }
}
