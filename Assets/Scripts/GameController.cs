using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public HUDController HUDController;
    private float segundosParaPVP = 10.0f;
    private Transform player;
    private int danoPlayer = 20;

    // Start is called before the first frame update
    void Start()
    {
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


    IEnumerator pvpOn()
    {
        //marca o tempo pro inicio do PVP
        yield return new WaitForSeconds(segundosParaPVP);
        HUDController.pvpOn();
        //VFX de inicio de PVP
    }
}
