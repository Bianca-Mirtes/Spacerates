using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.XPath;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public HUDController HUDController;
    public LifeBarController lifePlayer;
    public CombatController combatController;
    public GameObject background;
    public AudioClip backgroundSound;
    public CosmicDustController cosmicDustController;
    public RadarController radarController;
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
        //if(intance == null)
        //{
        //    intance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        //DontDestroyOnLoad(gameObject);

        background.GetComponent<AudioSource>().PlayOneShot(backgroundSound);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(pvpOn());
        //Set HUD
        HUDController.setAttack(danoPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        bool blackHole = player.GetComponent<PlayerController>().getBlackHole();
        if (blackHole)
        {
            //som de sendo sugado pelo buraco negro
            SceneManager.LoadScene(2);
        }

        bool isAlive = player.GetComponent<PlayerController>().getLife() > 0;
        if (!isAlive)
        {
            SceneManager.LoadScene(2);
        }

        if (combatController.getPlayersNum() == 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    public int getDano()
    {
        return danoPlayer;
    }

    public void setDano(int dano)
    {
        danoPlayer = dano;
        HUDController.setAttack(danoPlayer);
    }

    public void computeAttackPlayer(GameObject enemy, int xp)
    {
        enemy.GetComponent<EnemyController>().setLife(danoPlayer);
        if(enemy.GetComponent<EnemyController>().getLife() <= 0)
            player.GetComponent<PlayerController>().setXP(xp);
    }

    public void computeAttackEnemy(int dano)
    {
        if(player.GetComponent<PlayerController>().getLife() == 0)
        {
            Invoke("DeadPlayer", 1f);
            return;
        }
        player.GetComponent<PlayerController>().setLife(dano);
    }
    
    private void DeadPlayer()
    {
        // som da morte do player (por laser - explosão)
        SceneManager.LoadScene(2);
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
    
    public void changeLifeBar(float atual, float total)
    {
        HUDController.setLife(atual, total);
    }

    public void enableCosmicDust()
    {
        cosmicDustController.enableCosmicDust();
        radarController.disableRadar();
    }

    public void disableCosmicDust()
    {
        cosmicDustController.disableCosmicDust();
        radarController.enableRadar();
    }

    IEnumerator pvpOn()
    {
        //marca o tempo pro inicio do PVP
        yield return new WaitForSeconds(segundosParaPVP);
        HUDController.pvpOn();
        //VFX de inicio de PVP
    }
}
