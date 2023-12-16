using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject gema1;
    public GameObject gema2;
    public GameObject gema3;
    public GameObject gema4;

    // Start is called before the first frame update
    void Start()
    {
        gema1.SetActive(false);
        gema2.SetActive(false);
        gema3.SetActive(false);
        gema4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setRarityAsteroid()
    {
        int rarity = Random.Range(1, 101);
        if (rarity >= 1 && rarity <= 55)
        {
            spwanGem(1, 70, 95, 99);
        }
        else if (rarity >= 56 && rarity <= 80)
        {
            spwanGem(2, 55, 85, 95);
        }
        else if (rarity >= 81 && rarity <= 95)
        {
            spwanGem(3, 20, 40, 90);
        }
        else
        {
            spwanGem(4, 10, 25, 60);
        }
    }

    private void spwanGem(int rarity, int n1, int n2, int n3)
    {
        int gema = Random.Range(1, 101);
        if (gema >= 1 && gema < n1)
        {
            gema1.SetActive(true);
        }
        else if (gema >= n1+1 && gema <= n2)
        {
            gema2.SetActive(true);
        }
        else if (gema >= n2+1 && gema <= n3)
        {
            gema3.SetActive(true);
        }
        else
        {
            gema4.SetActive(true);
        }
    }
}
