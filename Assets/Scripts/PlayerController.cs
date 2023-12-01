using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Input input;
    private string statusPlayer = null;

    private float rayAttack = 4f;
    private GameObject attackController;
    public GameObject laser1;
    public GameObject laser2;

    public float speed = 3;
    public Sprite up;
    public Sprite right;
    public Sprite down;
    public Sprite left;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            Flip(0);
            statusPlayer = "right";
            gameObject.GetComponent<SpriteRenderer>().sprite = right;
        }
        if (horizontal < 0)
        {
            //Flip(1);
            statusPlayer = "left";
            gameObject.GetComponent<SpriteRenderer>().sprite = left;
        }
        if (vertical > 0)
        {
            Flip(2);
            statusPlayer = "up";
            gameObject.GetComponent<SpriteRenderer>().sprite = up;
        }
        if(vertical < 0)
        {
            //Flip(3);
            statusPlayer = "down";
            gameObject.GetComponent<SpriteRenderer>().sprite = down;

        }
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
    }

    private void verifAttack()
    {
        if (input.Player.Attack.IsPressed())
        {
            if (statusPlayer.Equals("right"))
            {
                GameObject clone = Instantiate(laser1, transform.GetChild(0).position, Quaternion.identity);
                clone.GetComponent<ConstantForce2D>().relativeForce = new Vector2(2, 0f);
            }else if (statusPlayer.Equals("left"))
            {
                GameObject clone = Instantiate(laser1, transform.GetChild(2).position, Quaternion.identity);
            }else if (statusPlayer.Equals("up"))
            {
                GameObject clone = Instantiate(laser2, transform.GetChild(3).position, Quaternion.identity);
                clone.GetComponent<ConstantForce2D>().relativeForce = new Vector2(0f, 2f);
            }
            else if (statusPlayer.Equals("down"))
            {
                GameObject clone = Instantiate(laser2, transform.GetChild(1).position, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 4f);
    }
}
