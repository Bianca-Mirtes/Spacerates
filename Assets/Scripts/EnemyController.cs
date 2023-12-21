using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class EnemyController : MonoBehaviour
{
    private int xpGenerated = 10;
    private int dano = 20;
    private Slider life;

    private string[] opcoes = { "right", "left", "up", "down" };
    private string status = "right";
    private float timeBetween = 3f;
    private bool canAttack = true;
    
    private Transform player;
    private float coordX;
    private float coordY;
    
    private float distanceFollow = 7f, distancePerception = 15f, distanceAttack = 1.5f;
    private float distanceForPlayer, distanceForAIPoint;

    public Transform[] destinyRandow;
    private int AIPointCurrent;
    private Animator ani;

    private bool followSomething, teste;
    private Vector3 localSpawn1;
    private Vector3 localSpawn2;
    private Vector3 localSpawn3;
    private Vector3 localSpawn4;

    private Transform right;
    private Transform up;
    private Transform down;
    private Transform left;

    public GameObject spawner;
    public GameObject laser1;
    public AudioClip laserSound;

    private int lvl;
    // Start is called before the first frame update
    void Start()
    {
        life = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        //ani = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AIPointCurrent = Random.Range(0, destinyRandow.Length);

        up = GameObject.Find("up").transform;
        down = GameObject.Find("down").transform;
        left = GameObject.Find("left").transform;
        right = GameObject.Find("right").transform;

        Instantiate(spawner, new Vector3(transform.position.x + 0.3f, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
        Instantiate(spawner, new Vector3(transform.position.x - 0.3f, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
        Instantiate(spawner, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Quaternion.identity);
        Instantiate(spawner, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Quaternion.identity);

        lvl = Random.Range(1, 4);
        if(lvl == 1)
        {
            life.maxValue = 80;
        }
        else if(lvl == 2)
        {
            life.maxValue = 96;
            life.value = 96;
            transform.localScale = transform.localScale * 1.2f;
        }
        else
        {
            life.maxValue = 116;
            life.value = 116;
            transform.localScale = transform.localScale * 1.4f;
        }
    }

    public int getLvl()
    {
        return lvl;
    }

    public float getLife()
    {
        return life.value;
    }

    private void Dead()
    {
        // animação de morte
        // som de morte
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(life.value == 0)
        {
            Invoke("Dead", 1f);
            return;
        }
        coordX = player.transform.position.x;
        coordY = player.transform.position.y;

        UpdatePositionSpawners();
        verifTimeForAttack();
        distanceForPlayer = Vector3.Distance(player.transform.position, transform.position);
        distanceForAIPoint = Vector3.Distance(destinyRandow[AIPointCurrent].transform.position, transform.position);

        if (distanceForPlayer > distancePerception)
        {
            Walking();
        }
        if (distanceForPlayer <= distanceFollow) // para verificar se o inimigo pode seguir o player
        {
            verifAttack();
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
            if (coordY < transform.position.y)
            {
                GameObject clone = Instantiate(laser1, localSpawn4, down.rotation);
                clone.GetComponent<LaserController>().setDirection(3);
            }else if (coordY > Mathf.Abs(transform.position.y))
            {
                GameObject clone = Instantiate(laser1, localSpawn3, up.rotation);
                clone.GetComponent<LaserController>().setDirection(2);
                clone.transform.Rotate(new Vector3(0f, 0f, 90f));
            }else if (coordX > Mathf.Abs(transform.position.x))
            {
                GameObject clone = Instantiate(laser1, localSpawn1, right.rotation);
                clone.GetComponent<LaserController>().setDirection(0);
            }else if (coordX < Mathf.Abs(transform.position.x))
            {
                GameObject clone = Instantiate(laser1, localSpawn2, right.rotation);
                clone.GetComponent<LaserController>().setDirection(1);
            }
            else
            {
                status = opcoes[Random.Range(0, 4)];
                if (status.Equals("right"))
                {
                    GameObject clone = Instantiate(laser1, localSpawn1, right.rotation);
                    clone.GetComponent<LaserController>().setDirection(0);
                }
                else if (status.Equals("left"))
                {
                    GameObject clone = Instantiate(laser1, localSpawn2, right.rotation);
                    clone.GetComponent<LaserController>().setDirection(1);
                }
                else if (status.Equals("up"))
                {
                    GameObject clone = Instantiate(laser1, localSpawn3, up.rotation);
                    clone.GetComponent<LaserController>().setDirection(2);
                    clone.transform.Rotate(new Vector3(0f, 0f, 90f));
                }
                else if (status.Equals("down"))
                {
                    GameObject clone = Instantiate(laser1, localSpawn4, down.rotation);
                    clone.GetComponent<LaserController>().setDirection(3);
                }
            }
            GetComponent<AudioSource>().PlayOneShot(laserSound);
            canAttack = false;
        }
    }

    void Walking()
    {
        //ani.SetBool("walking1", false);
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
            timeBetween = 3f;
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
        life.value -= value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            FindObjectOfType<GameController>().computeAttackPlayer(gameObject, xpGenerated);
            Destroy(collision.gameObject);
        }
    }
}
