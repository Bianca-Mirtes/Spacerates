using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Input input;
    private string statusPlayer = "right";
    private float timeBetween = 3f;
    private Animator ani;
    private bool canAttack = true;

    private int life = 100;
    private int xpPlayer = 0;

    public GameObject localSpawn1;
    public GameObject localSpawn2;
    public GameObject localSpawn3;
    public GameObject localSpawn4;

    private float rayAttack = 4f;
    private GameObject attackController;
    public GameObject laser1;

    public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void verifTimeForAttack()
    {
        if (timeBetween <= 0)
        {
            canAttack = true;
            timeBetween = 2f;
        }
        else
        {
            timeBetween -= Time.deltaTime;
        }
    }
    public int getLife()
    {
        return life;
    }

    public void setLife(int value)
    {
        life -= value;
    }
    public int getXP()
    {
        return xpPlayer;
    }

    public void setXP(int value)
    {
        xpPlayer += value;
    }

    private void Awake()
    {
        input = new Input();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = input.Player.MoveX.ReadValue<float>();
        float vertical = input.Player.MoveY.ReadValue<float>();

        //right/left
        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, 0f);
        //top/down
        rb.velocity = new Vector3(rb.velocity.x, vertical*speed, 0f);

        if(horizontal> 0)
        {
            ani.SetBool("walking1", true);
            Flip(0);
            statusPlayer = "right";
        }
        if (horizontal < 0)
        {
            ani.SetBool("walking1", true);
            Flip(1);
            statusPlayer = "left";
        }
        if (vertical > 0)
        {
            //ani.SetBool("walking2", true);
            //Flip(2);
            statusPlayer = "up";
        }
        if(vertical < 0)
        {
            //ani.SetBool("walking2", true);
            //Flip(3);
            statusPlayer = "down";

        }
        if(horizontal == 0 && vertical == 0)
        {
            ani.SetBool("walking1",false);
            //ani.SetBool("walking2", false);
        }
    }

    public string getStatus()
    {
        return statusPlayer;
    }
    private void Flip(int isFliped)
    {
        float scaleX = Mathf.Abs(transform.localScale.x);
        float scaleY = Mathf.Abs(transform.localScale.y);
        if (isFliped == 1)
        {
            scaleX *= -1;
        }
        if(isFliped == 3)
        {
            scaleY *= -1;
        }
        transform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);
    }

    private void Update()
    {
        verifAttack();
        verifTimeForAttack();
    }

    private void verifAttack()
    {
        if (canAttack)
        {
             if (input.Player.Attack.IsPressed())
             {
                if (statusPlayer.Equals("right"))
                {
                    GameObject clone = Instantiate(laser1, localSpawn1.transform.position, localSpawn1.transform.rotation);
                    clone.GetComponent<LaserController>().setDirection(0);
                }else if (statusPlayer.Equals("left"))
                {
                    GameObject clone = Instantiate(laser1, localSpawn2.transform.position, localSpawn1.transform.rotation);
                    clone.GetComponent<LaserController>().setDirection(1);
                }
                else if (statusPlayer.Equals("up"))
                {
                    GameObject clone = Instantiate(laser1, localSpawn3.transform.position, localSpawn3.transform.rotation);
                    clone.GetComponent<LaserController>().setDirection(2);
                    clone.transform.Rotate(new Vector3(0f, 0f, 90f));
                }
                else if (statusPlayer.Equals("down"))
                {
                    GameObject clone = Instantiate(laser1, localSpawn4.transform.position, localSpawn4.transform.rotation);
                    clone.GetComponent<LaserController>().setDirection(3);
                }
                canAttack = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 4f);
    }
}
