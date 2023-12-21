using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI agata;
    public TextMeshProUGUI ametista;
    public TextMeshProUGUI diamante;
    public TextMeshProUGUI esmeralda;

    public TextMeshProUGUI vida;
    public TextMeshProUGUI carga;

    public TextMeshProUGUI nivel;
    public TextMeshProUGUI durabilidadeAtual;
    public TextMeshProUGUI ataqueAtual;
    public TextMeshProUGUI velocidadeAtual;

    public TextMeshProUGUI jogadores;
    public TextMeshProUGUI pvp;

    public Image fillCarga;
    public Image ataque;
    public Sprite fill1;
    public Sprite fill2;

    public Color branco;
    public Color escuro;

    private int nivelAtual = 1;

    public GameObject atalhos;
    public ShopController shopController;

    public Image asteroideStts;
    public TextMeshProUGUI asteroideStts1;
    public TextMeshProUGUI asteroideStts2;
    // Start is called before the first frame update
    void Start()
    {
        setNivel(nivelAtual);
    }

    public void setJogadores(int players)
    {
        jogadores.text = "" + players;
    }

    public void atualizaQtddGemas(int agataQtdd, int ametistaQtdd, int diamanteQtdd, int esmeraldaQtdd)
    {
        agata.text = "" + agataQtdd;
        ametista.text = "" + ametistaQtdd;
        diamante.text = "" + diamanteQtdd;
        esmeralda.text = "" + esmeraldaQtdd;
    }

    public void setLife(float life, float maxLife)
    {
        vida.text = life + "/" + maxLife;
    }

    public void nextLvl()
    {
        nivelAtual++;
        setNivel(nivelAtual);
    }

    private void setNivel(int lvl)
    {
        nivel.text = ""+ lvl;
    }

    public void setAttack(int dano)
    {
        ataqueAtual.text = "Ataque: "+ dano;
    }

    public void changeSpeed(float speed)
    {
        float speedHUD = speed * 100 / 3;
        velocidadeAtual.text = "Velocidade: "+ speedHUD+" km/h";
    }

    public void setCarga(float cargaAtual, float cargaTotal)
    {
        carga.text = cargaAtual+"/"+cargaTotal;
        if(cargaAtual > cargaTotal)
        {
            fillCarga.sprite = fill2;
        }
        else
        {
            fillCarga.sprite = fill1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void visibilidade(bool estado)
    {
        GameObject[] objetosComATag = GameObject.FindGameObjectsWithTag("HUD");

        foreach (GameObject objeto in objetosComATag)
        {
            if (objeto.GetComponent<Image>() != null)
            {
                if (estado)
                {
                    objeto.GetComponent<Image>().color = branco;
                }
                else
                {
                    objeto.GetComponent<Image>().color = escuro;
                }
            }

            if (objeto.GetComponent<TextMeshProUGUI>() != null)
            {
                if (estado)
                {
                    objeto.GetComponent<TextMeshProUGUI>().color = branco;
                }
                else
                {
                    objeto.GetComponent<TextMeshProUGUI>().color = escuro;
                }
            }
        }
    }

    public void hasAttack(bool isAttackTime)
    {
        if(ataque != null)
        {
            if (!isAttackTime)
            {
                ataque.gameObject.SetActive(false);
            }
            if (isAttackTime)
            {
                ataque.gameObject.SetActive(true);
            }
        }
    }

    public void exibirAtalhos()
    {
        if (!atalhos.activeSelf)
        {
            shopController.disabledShop();
            atalhos.gameObject.SetActive(true);
        }
        else if(atalhos.activeSelf)
        {
            desligarAtalhos();
        }
    }

    public void desligarAtalhos()
    {
        atalhos.gameObject.SetActive(false);
    }

    public void setAsteroidStatus(GameObject asteroid)
    {
        if(asteroid != null)
        {
            asteroideStts.gameObject.SetActive(true);
            asteroideStts1.gameObject.SetActive(true);
            asteroideStts2.gameObject.SetActive(true);

            string stts1 = "";
            string stts2 = "";

            int raridade = asteroid.GetComponent<AsteroidController>().getRaridade();
            if (raridade == 1)
            {
                stts1 = "comum";
                stts2 = "Altas chacnes de ser uma ágata";
            }
            else if (raridade == 2)
            {
                stts1 = "incomum";
                stts2 = "";
            }
            else if (raridade == 3)
            {
                stts1 = "raro";
                stts2 = "50% de chance de esmeralda";
            }
            else if (raridade == 4)
            {
                stts1 = "super raro";
                stts2 = "Boas chances de ser um diamante";
            }
            asteroideStts1.text = "Asteroide "+stts1;
            asteroideStts2.text = stts2;
        }
        else
        {
            asteroideStts.gameObject.SetActive(false);
            asteroideStts1.gameObject.SetActive(false);
            asteroideStts2.gameObject.SetActive(false);
        }
    }

    public void pvpOn()
    {
        pvp.text = "ON";
    }
}