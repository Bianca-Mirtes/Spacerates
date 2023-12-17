using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.Windows;
using UnityEditor.Tilemaps;

public class EnemyController : MonoBehaviour
{
    private int life = 80;
    private int xpGenerated = 10;
    private int dano = 20;
    private string[] opcoes = { "right", "left", "up", "down" };
    private string status = "right";
    private float timeBetween = 4f;
    private bool canAttack = true;
    public GameObject laser1;
    private Transform player;

    private float distanceFollow = 7f, distancePerception = 15f, distanceAttack = 1.5f;

    private float timeForAttack = 1.5f;
    private float distanceForPlayer, distanceForAIPoint;

    public Transform[] destinyRandow;
    private int AIPointCurrent;

    private bool followSomething, teste;
    private Vector3 localSpawn1;
    private Vector3 localSpawn2;
    private Vector3 localSpawn3;
    private Vector3 localSpawn4;

    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AIPointCurrent = Random.Range(0, destinyRandow.Length);
        Instantiate(spawner, new Vector3(transform.position.x + 0.3f, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
        Instantiate(spawner, new Vector3(transform.position.x - 0.3f, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
        Instantiate(spawner, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Quaternion.identity);
        Instantiate(spawner, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        status = opcoes[Random.Range(0, 4)];
        verifAttack();
        verifTimeForAttack();
        UpdatePositionSpawners();
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

    private void UpdatePositionSpawners()
    {
        localSpawn1 = new Vector3(transform.position.x + 0.3f, transform.position.y - 0.2f, transform.position.z);
        localSpawn2 = new Vector3(transform.position.x - 0.3f, transform.position.y - 0.2f, transform.position.z);
        localSpawn3 = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        localSpawn4 = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
    }

    private void verifAttack()
    {
        if (canAttack)
        {
            if (status.Equals("right"))
            {
                GameObject clone = Instantiate(laser1, localSpawn1, Quaternion.identity);
                clone.GetComponent<LaserController>().setDirection(0);
            }
            else if (status.Equals("left"))
            {
                GameObject clone = Instantiate(laser1, localSpawn2, Quaternion.identity);
                clone.GetComponent<LaserController>().setDirection(1);
            }
            else if (status.Equals("up"))
            {
                GameObject clone = Instantiate(laser1, localSpawn3, Quaternion.identity);
                clone.GetComponent<LaserController>().setDirection(2);
                clone.transform.Rotate(new Vector3(0f, 0f, 90f));
            }
            else if (status.Equals("down"))
            {
                GameObject clone = Instantiate(laser1, localSpawn4, Quaternion.identity);
                clone.GetComponent<LaserController>().setDirection(3);
            }
            canAttack = false;
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
    private void verifTimeForAttack()
    {
        if (timeBetween <= 0)
        {
            canAttack = true;
            timeBetween = 4f;
        }
        else
        {
            timeBetween -= Time.deltaTime;
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
