using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.x;

        transform.position = newPosition;
    }
}
