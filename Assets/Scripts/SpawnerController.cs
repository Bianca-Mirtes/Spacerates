using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.name.Equals("right"))
        {
            transform.position = new Vector3(player.position.x + 0.3f, player.position.y - 0.2f, player.position.z);
        }
        if (transform.name.Equals("left"))
        {
            transform.position = new Vector3(player.position.x - 0.3f, player.position.y - 0.2f, player.position.z);
        }
        
        if (transform.name.Equals("up"))
        {
            transform.position = new Vector3(player.position.x, player.position.y + 0.3f, player.position.z);
        }
        if (transform.name.Equals("down"))
        {
            transform.position = new Vector3(player.position.x, player.position.y - 0.3f, player.position.z);
        }
    }
}
