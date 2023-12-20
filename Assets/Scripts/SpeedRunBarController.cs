using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedRunBarController : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Slider>().value = total;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float speedRunAtual = player.GetComponent<PlayerController>().getSpeedRun();
        //float speedRunMax = player.GetComponent<PlayerController>().getMaxLife();
        gameObject.GetComponent<Slider>().value = speedRunAtual;
        //gameObject.GetComponent<Slider>().maxValue = speedRunMax;
    }
}
