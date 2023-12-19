using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HUDController HUDController;
    public ShopController shopController;
    private bool shopEnable = false;

    private Rigidbody2D rb;
    private Input input;
    private string statusPlayer = "right";
    private float timeBetween = 2f;
    private Animator ani;
    private bool direcaoHorizontal = true;
    private bool canAttack = true;

    private int life;
    private int maxLife;
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

    private int agataQtdd = 0;
    private int ametistaQtdd = 0;
    private int diamanteQtdd = 0;
    private int esmeraldaQtdd = 0;

    private Vector2 buracoNegro = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        life = 100;
        maxLife = 100;

        speedReference = speed;
        HUDController.changeSpeed(speed);
        HUDController.atualizaQtddGemas(agataQtdd, ametistaQtdd, diamanteQtdd, esmeraldaQtdd);
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
        life = value;
    }

    public int getMaxLife()
    {
        return maxLife;
    }

    public void setMaxLife(int value)
    {
        maxLife = value;
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
    }

    public float getSpeed()
    {
        return speed;
    }

    public void setSpeed(float velocidade)
    {
        speedReference = velocidade;
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
            if(statusPlayer == "up" || statusPlayer == "down")
                statusPlayer = "right";
        }
        else
            ani.speed = 1;
    }

    public void addGema(int gema)
    {
        if (gema == 1)
        {
            agataQtdd++;
        }
        else if (gema == 2)
        {
            ametistaQtdd++;
        }
        else if (gema == 3)
        {
            diamanteQtdd++;
        }
        else if (gema == 4)
        {
            esmeraldaQtdd++;
        }
        HUDController.atualizaQtddGemas(agataQtdd, ametistaQtdd, diamanteQtdd, esmeraldaQtdd);
    }

    public void resetGem(int gem)
    {
        if(gem == 1)
        {
            cargaAtual -= agataQtdd * 10;
            agataQtdd = 0;
        }
        if (gem == 2)
        {
            cargaAtual -= ametistaQtdd * 15;
            ametistaQtdd = 0;
        }
        if (gem == 3)
        {
            cargaAtual -= diamanteQtdd * 20;
            diamanteQtdd = 0;
        }
        if (gem == 4)
        {
            cargaAtual -= esmeraldaQtdd * 25;
            esmeraldaQtdd = 0;
        }
        HUDController.atualizaQtddGemas(agataQtdd, ametistaQtdd, diamanteQtdd, esmeraldaQtdd);
    }

    public int getGemNumber(int gem)
    {
        if(gem == 1)
            return agataQtdd;
        else if(gem == 2)
            return ametistaQtdd;
        else if(gem == 3)
            return diamanteQtdd;
        else if(gem == 4)
            return esmeraldaQtdd;
        else
            return 0;
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

        if(buracoNegro != Vector2.zero)
            transform.position = Vector2.MoveTowards(transform.position, buracoNegro, 5f * Time.deltaTime);

        //abrir loja
        if (shopController.gameObject.activeSelf && !shopEnable)
        {
            shopEnable = true;
        }

        if (input.Player.Loja.triggered){
            if (shopEnable)
            {
                shopController.disabledShop();
                shopEnable = false;
            }
            else
            {
                shopController.enabledShop();
                shopEnable = true;
            }
        }
        

        //mecanica de reduzir velocidade com peso
        if (cargaAtual > carga)
        {
            if (speed == speedReference)
            {
                speed = speed / 2;
                HUDController.changeSpeed(speed);
                coletaEnable = false;
            }
                
        }
        else if (speed != speedReference)
        {
            speed = speedReference;
            HUDController.changeSpeed(speed);
            coletaEnable = true;
        }
    }

    public bool getBlackHole()
    {
        return buracoNegro != Vector2.zero;
    }

    private void verifAttack()
    {
        if (canAttack)
        {
             if (input.Player.Attack.triggered)
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
        if (collision.gameObject.layer == 9)
        {
            Transform target = collision.GetComponentInChildren<Transform>().GetChild(0).GetComponentInChildren<Transform>();
            buracoNegro = new Vector2(target.position.x, target.position.y);
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
