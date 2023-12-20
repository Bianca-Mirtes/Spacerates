using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float vidaAtual = player.GetComponent<PlayerController>().getLife();
        float vidaMax   = player.GetComponent<PlayerController>().getMaxLife();
        gameObject.GetComponent<Slider>().value = vidaAtual;
        gameObject.GetComponent<Slider>().maxValue = vidaMax;
        FindObjectOfType<GameController>().changeLifeBar(vidaAtual, vidaMax);
    }
}
