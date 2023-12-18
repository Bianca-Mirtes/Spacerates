using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Input input;
    private string statusPlayer = "right";
    private float timeBetween = 2f;
    private Animator ani;
    private bool direcaoHorizontal = true;
    private bool canAttack = true;

    private int life = 100;
    private int xpPlayer = 0;

    public GameObject localSpawn1;
    public GameObject localSpawn2;
    public GameObject localSpawn3;
    public GameObject localSpawn4;

    public PolygonCollider2D colliderH;
    public PolygonCollider2D colliderV;

    private float rayAttack = 4f;
    private GameObject attackController;
    public GameObject laser1;

    public float speed = 3;
    private float speedReference = 3;
    private int carga = 200;
    private int cargaAtual = 0;
    private bool coletaEnable = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        speedReference = speed;
        FindObjectOfType<GameController>().changeSpeed(speed);
        setCarga(carga);
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
        FindObjectOfType<GameController>().setVidaHud(life);
    }
    public int getXP()
    {
        return xpPlayer;
    }

    public void setXP(int value)
    {
        xpPlayer += value;
    }

    public int getCarga()
    {
        return carga;
    }

    public void setCarga(int newCarga)
    {
        carga = newCarga;
    }

    public void setCargaAtual(int pesoAdiicionado)
    {
        cargaAtual += pesoAdiicionado;
    }

    public int getCargaAtual()
    {
        return cargaAtual;
    }

    public void nextLvl()
    {
        xpPlayer = 0;
        //carga += Mathf.Abs((carga * 2 / 10));
        //setCarga(carga);
        //speedReference = speedReference + 1;
        //speed = speedReference;
        //modificar valores velocidade+tanto...
    }

    public float getSpeed()
    {
        return speed;
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
            ani.SetBool("walking2", false);
            Flip(0);
            statusPlayer = "right";

            if (!direcaoHorizontal)
                changeCollider(0);
        }
        if (horizontal < 0)
        {
            ani.SetBool("walking1", true);
            ani.SetBool("walking2", false);
            Flip(1);
            statusPlayer = "left";

            if (!direcaoHorizontal)
                changeCollider(0);
        }
        if (vertical > 0)
        {
            ani.SetBool("walking1", false);
            ani.SetBool("walking2", true);
            Flip(0);
            statusPlayer = "up";
            
            if (direcaoHorizontal)
                changeCollider(1);
        }
        if(vertical < 0)
        {
            ani.SetBool("walking1", false);
            ani.SetBool("walking2", true);
            Flip(3);
            statusPlayer = "down";

            if (direcaoHorizontal)
                changeCollider(1);
        }
        if (horizontal == 0 && vertical == 0)
        {
            ani.SetBool("walking1", false);
            ani.SetBool("walking2", false);
            ani.Play("idleHorizontal", 0, 0f);
            float resetY = Mathf.Abs(transform.localScale.y); //Corrige bug do navio de cabeca pra baixo
            transform.localScale = new Vector3(transform.localScale.x, resetY, transform.localScale.z);
            ani.speed = 0; //Desliga os motores rsrs
            changeCollider(0);
        }
        else
            ani.speed = 1;
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

    private void changeCollider(int direcao)
    {
        if(direcao == 0)
        {
            if (colliderH != null)
            {
                PolygonCollider2D colliderDestino = gameObject.GetComponent<PolygonCollider2D>();
                colliderDestino.points = colliderH.points;
                direcaoHorizontal = true;
            }
        }
        else {
            if (colliderV != null)
            {
                PolygonCollider2D colliderDestino = gameObject.GetComponent<PolygonCollider2D>();
                colliderDestino.points = colliderV.points;
                direcaoHorizontal = false;
            }
        }
    }

    private void Update()
    {
        verifAttack();
        verifTimeForAttack();

        //mecanica de reduzir velocidade com peso
        if (cargaAtual > carga)
        {
            if (speed == speedReference)
            {
                speed = speed / 2;
                FindObjectOfType<GameController>().changeSpeed(speed);
                coletaEnable = false;
            }
                
        }
        else if (speed != speedReference)
        {
            speed = speedReference;
            FindObjectOfType<GameController>().changeSpeed(speed);
            coletaEnable = true;
        }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            FindObjectOfType<GameController>().computeAttackEnemy(20);
            Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (input.Player.Coleta.IsPressed() && coletaEnable)
            {
                //som de coleta
                FindObjectOfType<GameController>().updateJewels(collision.gameObject.name);
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.layer == 8)
        {
            //som de po
            FindObjectOfType<GameController>().enableCosmicDust();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            FindObjectOfType<GameController>().disableCosmicDust();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 4f);
    }
}
