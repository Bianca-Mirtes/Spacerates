using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public TextMeshProUGUI moedas;

    public TextMeshProUGUI moedas1;
    public TextMeshProUGUI moedas2;
    public TextMeshProUGUI moedas3;
    public TextMeshProUGUI moedas4;

    public Button venderMoedas1;
    public Button venderMoedas2;
    public Button venderMoedas3;
    public Button venderMoedas4;

    public Color enableColor;
    public Color disableColor;

    private PlayerController player;
    private bool shopEnable = false;
    private int valor1, valor2, valor3, valor4;
    private int totalDeMoedas;


    // Start is called before the first frame update
    void Start()
    {
        valor1 = 1;
        valor2 = 2;
        valor3 = 4;
        valor4 = 6;
        totalDeMoedas = 0;
        moedas.text = totalDeMoedas + " moeda(s)";
        gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shopEnable)
        {
            moedas1.text = player.getGemNumber(1) * valor1 + " moeda(s)";
            moedas2.text = player.getGemNumber(2) * valor2 + " moeda(s)";
            moedas3.text = player.getGemNumber(3) * valor3 + " moeda(s)";
            moedas4.text = player.getGemNumber(4) * valor4 + " moeda(s)";

            enableBtn(player.getGemNumber(1), venderMoedas1);
            enableBtn(player.getGemNumber(2), venderMoedas2);
            enableBtn(player.getGemNumber(3), venderMoedas3);
            enableBtn(player.getGemNumber(4), venderMoedas4);
        }
    }

    private void enableBtn(int numGem, Button btn)
    {
        if (numGem == 0)
        {
            btn.interactable = false;
            btn.GetComponentInChildren<TextMeshProUGUI>().color = disableColor;
        }
        else
        {
            btn.interactable = true;
            btn.GetComponentInChildren<TextMeshProUGUI>().color = enableColor;
        }
            
    }

    public void vender(int gem)
    {
        if (gem == 1)
        {
            totalDeMoedas += player.getGemNumber(1) * valor1;
            player.resetGem(1);
        }
        if (gem == 2)
        {
            totalDeMoedas += player.getGemNumber(2) * valor2;
            player.resetGem(2);
        }
        if (gem == 3)
        {
            totalDeMoedas += player.getGemNumber(3) * valor3;
            player.resetGem(3);
        }
        if (gem == 4)
        {
            totalDeMoedas += player.getGemNumber(4) * valor4;
            player.resetGem(4);
        }

        moedas.text = totalDeMoedas + " moeda(s)";
    }

    public void enabledShop()
    {
        gameObject.SetActive(true);
        shopEnable = true;
    }

    public void disabledShop()
    {
        gameObject.SetActive(false);
        shopEnable = false;
    }
}
