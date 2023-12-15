using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidField : MonoBehaviour
{
    public GameObject container;
    public GameObject[] asteroids;
    public int totalDeAsteroides = 50;
    public int distanceBetween = 3;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector2> pontos = generatePoints();
        generateAsteroids(pontos);
    }

    private List<Vector2> generatePoints()
    {
        List<Vector2> paresOrdenados = new List<Vector2>();
        for (int i = 0; i < totalDeAsteroides; i++)
        {
            float coordenadaX = Random.Range(-15, 16) * distanceBetween;
            float coordenadaY = Random.Range(-15, 16) * distanceBetween;

            if(!paresOrdenados.Contains(new Vector2(coordenadaX, coordenadaY)))
                paresOrdenados.Add(new Vector2(coordenadaX, coordenadaY));
            
            //Debug.Log("Par " + (i + 1) + ": (" + coordenadaX + ", " + coordenadaY + ")");
        }

        return paresOrdenados;
    }

    private void generateAsteroids(List<Vector2> pontos)
    {
        if (container != null && asteroids[0] != null && asteroids[1] != null)
        {
            foreach (var ponto in pontos)
            {
                Vector3 spawnPosition = new Vector3(ponto.x, ponto.y, 0f);
                if(spawnPosition != Vector3.zero)
                    Instantiate(asteroids[Random.Range(0, asteroids.Length)], spawnPosition, Quaternion.identity, container.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
