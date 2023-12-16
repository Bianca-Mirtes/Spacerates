using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int life = 80;
    private int xpGenerated = 10;
    private Transform player;

    private float distanceFollow = 7f, distancePerception = 15f, distanceAttack = 1.5f;

    private float timeForAttack = 1.5f;
    private float distanceForPlayer, distanceForAIPoint;

    public Transform[] destinyRandow;
    private int AIPointCurrent;

    private bool followSomething, teste;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AIPointCurrent = Random.Range(0, destinyRandow.Length);
    }

    // Update is called once per frame
    void Update()
    {
        distanceForPlayer = Vector3.Distance(player.transform.position, transform.position);
        distanceForAIPoint = Vector3.Distance(destinyRandow[AIPointCurrent].transform.position, transform.position);

        if (distanceForPlayer > distancePerception)
        {
            Walking();
        }
        if (distanceForPlayer <= distanceFollow) // para verificar se o inimigo pode seguir o player
        {
            Follow();
            followSomething = true;
        }
        else
        {
            Walking();
            followSomething = false;
        }
    }

    void Walking()
    {
        if (!followSomething)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinyRandow[AIPointCurrent].transform.position, 0.8f * Time.deltaTime);
            if (transform.position.Equals(destinyRandow[AIPointCurrent].transform.position))
            {
                AIPointCurrent = Random.Range(0, destinyRandow.Length);

            }
        }
        else
        {
            teste = true;
        }
    }

    void Follow()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1.5f * Time.deltaTime);
    }
    public void setLife(int value)
    {
        life -= value; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            FindObjectOfType<GameController>().computeAttackPlayer(gameObject, xpGenerated);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
