using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarController : MonoBehaviour
{
    private Transform player;
    public int total = 100;
    public int incremento = 20;

    public ShopController shopController;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Slider>().value = total;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Slider>().value = player.GetComponent<PlayerController>().getXP();
        float valorMax = gameObject.GetComponent<Slider>().maxValue;
        //passar de nivel
        if (gameObject.GetComponent<Slider>().value == valorMax)
        {
            //Xp do proximo nivel
            gameObject.GetComponent<Slider>().maxValue = valorMax + incremento;

            //CHAMAR LOJINHA
            shopController.enabledShop();

            //Reset
            player.GetComponent<PlayerController>().nextLvl();
            FindObjectOfType<GameController>().nextLvl();
            //float speed = player.GetComponent<PlayerController>().getSpeed();
        }
    }
}
