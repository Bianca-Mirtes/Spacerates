using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.Windows;

public class LaserController : MonoBehaviour
{

    public float speed;
    private int direction;
    private Transform player;
    private bool causesDamage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void setDirection(int value)
    {
        direction = value;
    }

    void Update()
    {
        //verifScopeLaser();
        if(direction == 0)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if(direction == 1)
        {
            float scaleX = Mathf.Abs(transform.localScale.x);
            scaleX *= -1;
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if(direction == 2)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (direction == 3)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        Destroy(gameObject, 3.5f);
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
            //Destroy(gameObject);
        }
        return causesDamage;
    }
}
