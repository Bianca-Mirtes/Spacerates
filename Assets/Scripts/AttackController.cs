using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Transform player;
    private bool causesDamage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        verifScopeLaser();
    }

    private bool verifScopeLaser()
    {
        Collider2D colliderLaser = Physics2D.OverlapCircle(player.position, 4f, gameObject.layer);
        if (colliderLaser != null)
        {
            causesDamage = true;
        }
        else
        {
            causesDamage = false;
            Destroy(colliderLaser.gameObject);
        }
        return causesDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("asteroide"))
        {
            Destroy(gameObject);
        }
    }
}
