using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.XPath;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public HUDController HUDController;
    public GameObject background;
    public AudioClip backgroundSound;
    public CosmicDustController cosmicDustController;
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

        background.GetComponent<AudioSource>().PlayOneShot(backgroundSound);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(pvpOn());
        //Set HUD
        HUDController.setAttack(danoPlayer);
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
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (joia.Equals("Ágata"))
        {
            playerController.addGema(1);
            playerController.setCargaAtual(10);
            playerController.setXP(10);
        }
        if (joia.Equals("Ametista"))
        {
            playerController.addGema(2);
            playerController.setCargaAtual(15);
            playerController.setXP(15);
        }
        if (joia.Equals("Diamante"))
        {
            playerController.addGema(3);
            playerController.setCargaAtual(20);
            playerController.setXP(20);
        }
        if (joia.Equals("Esmeralda"))
        {
            playerController.addGema(4);
            playerController.setCargaAtual(25);
            playerController.setXP(25);
        }
    }

    public void nextLvl()
    {
        HUDController.nextLvl();
    }

    public void changeCargaBar(float atual, float total)
    {
        HUDController.setCarga(atual, total);
    }

    public void enableCosmicDust()
    {
        cosmicDustController.enableCosmicDust();
        //desativar radar!
    }

    public void disableCosmicDust()
    {
        cosmicDustController.disableCosmicDust();
        //reativar radar!
    }

    IEnumerator pvpOn()
    {
        //marca o tempo pro inicio do PVP
        yield return new WaitForSeconds(segundosParaPVP);
        HUDController.pvpOn();
        //VFX de inicio de PVP
    }
}
