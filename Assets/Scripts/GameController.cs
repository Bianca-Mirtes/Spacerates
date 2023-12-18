using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.XPath;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public HUDController HUDController;
    private float segundosParaPVP = 10.0f;
    private Transform player;
    private int danoPlayer = 20;

    private int countPurple = 0;
    private int countGreen = 0;
    private int countBlue = 0;
    private int countPink = 0;

    private static GameController intance = null;

    // Start is called before the first frame update
    void Start()
    {
        if(intance == null)
        {
            intance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(pvpOn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void computeAttackPlayer(GameObject enemy, int xp)
    {
        enemy.GetComponent<EnemyController>().setLife(danoPlayer);
        player.GetComponent<PlayerController>().setXP(xp);
    }

    public void computeAttackEnemy(int dano)
    {
        player.GetComponent<PlayerController>().setLife(dano);
    }

    public void updateJewels(string joia)
    {
        if (joia.Equals("Ágata"))
        {
            HUDController.addGema(1);
        }
        if (joia.Equals("Ametista"))
        {
            HUDController.addGema(2);
        }
        if (joia.Equals("Diamante"))
        {
            HUDController.addGema(3);  
        }
        if (joia.Equals("Esmeralda"))
        {
            HUDController.addGema(4);
        }
    }

    public void setVidaHud(int life)
    {
        HUDController.setLife(life);
    }

    IEnumerator pvpOn()
    {
        //marca o tempo pro inicio do PVP
        yield return new WaitForSeconds(segundosParaPVP);
        HUDController.pvpOn();
        //VFX de inicio de PVP
    }
}
