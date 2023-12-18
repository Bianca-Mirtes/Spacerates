using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargaBarController : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        int total = player.GetComponent<PlayerController>().getCarga();
        gameObject.GetComponent<Slider>().maxValue = total;
        gameObject.GetComponent<Slider>().value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float cargaAtual = player.GetComponent<PlayerController>().getCargaAtual();
        float cargaTotal = player.GetComponent<PlayerController>().getCarga();
        gameObject.GetComponent<Slider>().value = cargaAtual;
        gameObject.GetComponent<Slider>().maxValue = cargaTotal;
        FindObjectOfType<GameController>().changeCargaBar(cargaAtual, cargaTotal);
        //gameObject.GetComponent<Slider>().value = player.GetComponent<PlayerController>().getLife();
    }
}
