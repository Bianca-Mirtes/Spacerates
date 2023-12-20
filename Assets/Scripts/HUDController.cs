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
        velocidadeAtual.text = "Velocidade: "+speed;
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
            if (ataque.gameObject.activeSelf && !isAttackTime)
            {
                ataque.gameObject.SetActive(false);
            }
            if (!ataque.gameObject.activeSelf && isAttackTime)
            {
                ataque.gameObject.SetActive(true);
            }
        }
    }

    public void pvpOn()
    {
        pvp.text = "ON";
    }
}