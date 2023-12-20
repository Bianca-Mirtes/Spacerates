using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Transform player;

    private float minX = -25.6f, maxX = 27f;
    private float minY = -31.5f, maxY = 28.05f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 newPosition = player.position + new Vector3(0f, 0f, -10f);
        transform.position = newPosition;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
                                 Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
    }
}
