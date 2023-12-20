using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public GameObject shop;

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
    public TextMeshProUGUI precoDoNivel;

    public Color enableColor;
    public Color disableColor;

    private PlayerController player;
    private GameController gameController;
    private HUDController HUDController;
    private bool shopEnable = false;
    private int valor1, valor2, valor3, valor4;

    private int preco;

    public int percentual1;
    public int percentual2;

    private int totalDeMoedas;
    private int totalDePontos;

    private int moedasAReceber;

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
        shop.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameController = FindObjectOfType<GameController>().GetComponent<GameController>();
        HUDController = FindObjectOfType<HUDController>().GetComponent<HUDController>();

        preco = 24;
        setPreco();
    }

    private void setPreco()
    {
        precoDoNivel.text = preco + " moedas e 1 ponto de XP";
    }

    // Update is called once per frame
    void Update()
    {
        int oldMoedasAReceber = moedasAReceber;
        moedasAReceber = player.getGemNumber(1) * valor1 + player.getGemNumber(2) * valor2
                       + player.getGemNumber(3) * valor3 + player.getGemNumber(4) * valor4;

        if (totalDePontos > 0 && (moedasAReceber > oldMoedasAReceber) && moedasAReceber + totalDeMoedas >= preco)
        {
            enabledShop();
        }

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

            enableBtnCompra(preco, comprar1);
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
            foreach (Transform child in btn.transform)
            {
                TextMeshProUGUI textMesh = child.GetComponent<TextMeshProUGUI>();
                if (textMesh != null)
                {
                    textMesh.color = disableColor;
                }
            }
        }
        else
        {
            btn.interactable = true;
            btn.GetComponentInChildren<TextMeshProUGUI>().color = enableColor;
            foreach (Transform child in btn.transform)
            {
                TextMeshProUGUI textMesh = child.GetComponent<TextMeshProUGUI>();
                if (textMesh != null)
                {
                    textMesh.color = enableColor;
                }
            }
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

    public void comprarLvl()
    {
        totalDeMoedas -= preco;

        //vida
        if (player.getLife() == player.getMaxLife())
        {
            player.setLife(player.getLife() + Mathf.Abs((player.getLife() * percentual2 / 10)));
            player.setMaxLife(player.getMaxLife() + Mathf.Abs((player.getMaxLife() * percentual2 / 10)));
        }else if(player.getLife() < player.getMaxLife())
        {
            int valor = player.getLife() + Mathf.Abs((player.getLife() * percentual2 / 10));
            valor = Mathf.Max(valor, 0);
            player.setLife(valor);
        }

        //carga
        player.setCarga(player.getCarga() + Mathf.Abs((player.getCarga() * percentual1 / 10)));

        //dano
        gameController.setDano(gameController.getDano() + Mathf.Abs((gameController.getDano() * percentual1 / 10)));

        //velocidade
        player.setSpeed(player.getSpeed() - Mathf.Abs((player.getSpeed() * percentual2 / 10)));

        //tamanho
        player.increaseSize();

        totalDePontos--;
        preco = Mathf.Abs((int)(preco * 1.5f));
        setPreco();
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
        shop.SetActive(true);
        shopEnable = true;
        HUDController.desligarAtalhos();
    }

    public void disabledShop()
    {
        shop.SetActive(false);
        shopEnable = false;
    }
}
