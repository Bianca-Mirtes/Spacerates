using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public HUDController HUDController;
    private float segundosParaPVP = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(pvpOn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator pvpOn()
    {
        //marca o tempo pro inicio do PVP
        yield return new WaitForSeconds(segundosParaPVP);
        HUDController.pvpOn();
        //VFX de inicio de PVP
    }
}
