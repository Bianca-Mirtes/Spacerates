using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public HUDController HUDController;
    public GameObject enemiesContainer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HUDController.setJogadores(getPlayersNum());
    }

    public int getPlayersNum()
    {
        return enemiesContainer.transform.childCount;
    }
}
