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


    private int agataQtdd = 0;
    private int ametistaQtdd = 0;
    private int diamanteQtdd = 0;
    private int esmeraldaQtdd = 0;

    private int cargaTotal = 200;
    private int cargaAtual = 0;
    private int nivelAtual = 1;

    // Start is called before the first frame update
    void Start()
    {
        atualizaQtddGemas();
        setLife(100);
        setNivel(nivelAtual);

        carga.text = "0/"+ cargaTotal;

        durabilidadeAtual.text = "Durabilidade: 0/0";
        ataqueAtual.text = "Ataque: 0/0";
        velocidadeAtual.text = "Velocidade: 0";
    }

    private void atualizaQtddGemas()
    {
        agata.text = "" + agataQtdd;
        ametista.text = "" + ametistaQtdd;
        diamante.text = "" + diamanteQtdd;
        esmeralda.text = "" + esmeraldaQtdd;
    }

    public void setLife(int life)
    {
        vida.text = life + "/100";
    }

    private void setNivel(int lvl)
    {
        nivel.text = ""+ lvl;
    }

    public void setCargaTotal(int cargaTotal)
    {
        this.cargaTotal = cargaTotal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addGema(int gema)
    {
        int peso = 0;
        if (gema == 1)
        {
            agataQtdd++;
        }
        else if(gema == 2)
        {
            ametistaQtdd++;
        }
        else if (gema == 3)
        {
            diamanteQtdd++;
        }
        else if (gema == 4)
        {
            esmeraldaQtdd++;
        }
        atualizaQtddGemas();
        //calcularCarga();
    }

    public void pvpOn()
    {
        pvp.text = "ON";
    }
}