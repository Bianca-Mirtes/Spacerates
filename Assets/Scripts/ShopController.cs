using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public TextMeshProUGUI moedasEPontos;

    public TextMeshProUGUI moedas1;
    public TextMeshProUGUI moedas2;
    public TextMeshProUGUI moedas3;
    public TextMeshProUGUI moedas4;

    public Button venderMoedas1;
    public Button venderMoedas2;
    public Button venderMoedas3;
    public Button venderMoedas4;

    public Button comprar1;
    public Button comprar2;
    public Button comprar3;
    public Button comprar4;
    public Button comprar5;

    public Color enableColor;
    public Color disableColor;

    private PlayerController player;
    private bool shopEnable = false;
    private int valor1, valor2, valor3, valor4;

    public int preco1, preco2, preco3, preco4, preco5;
    private int totalDeMoedas;
    private int totalDePontos;

    // Start is called before the first frame update
    void Start()
    {
        valor1 = 1;
        valor2 = 2;
        valor3 = 4;
        valor4 = 6;

        totalDeMoedas = 0;
        totalDePontos = 0;
        atualizarRecursos();
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

            enableBtnVenda(player.getGemNumber(1), venderMoedas1);
            enableBtnVenda(player.getGemNumber(2), venderMoedas2);
            enableBtnVenda(player.getGemNumber(3), venderMoedas3);
            enableBtnVenda(player.getGemNumber(4), venderMoedas4);

            enableBtnCompra(preco1, comprar1);
            enableBtnCompra(preco2, comprar2);
            enableBtnCompra(preco3, comprar3);
            enableBtnCompra(preco4, comprar4);
            enableBtnCompra(preco5, comprar5);
        }
    }

    private void enableBtnVenda(int numGem, Button btn)
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

    private void enableBtnCompra(int preco, Button btn)
    {
        if (totalDePontos == 0 || preco > totalDeMoedas)
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

    public void disableBtnCompra(Button btn)
    {
        //StartCoroutine()
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
        atualizarRecursos();
    }

    public void comprar(int atributo)
    {
        if (atributo == 1)
        {
            totalDeMoedas -= preco1;
            //player.improveLife();
        }else
        if (atributo == 2)
        {
            totalDeMoedas -= preco2;
            //player.improveCarga();
        }else
        if (atributo == 3)
        {
            totalDeMoedas -= preco3;
            //player.improveAtaque();
        }else
        if (atributo == 4)
        {
            totalDeMoedas -= preco4;
            //player.improveVelocidade();
        }else
        if (atributo == 5)
        {
            totalDeMoedas -= preco5;
            //player.improveEspecial();
        }
        totalDePontos--;
        atualizarRecursos();
    }

    public void addPoint()
    {
        totalDePontos++;
        atualizarRecursos();
    }

    private void atualizarRecursos()
    {
        moedasEPontos.text = totalDeMoedas + " moeda(s) e " + totalDePontos + " ponto(s) de XP";
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