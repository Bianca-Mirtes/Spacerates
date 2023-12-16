using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarController : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Slider>().value = 100;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Slider>().value = player.GetComponent<PlayerController>().getXP();
    }
}
