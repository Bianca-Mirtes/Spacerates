using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CelestialBodyField: MonoBehaviour
{
    public GameObject container;
    public GameObject[] objetos;
    public int total = 70;
    public int distanceBetween = 2;
    private float variacao = 0.95f;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector2> pontos = generatePoints();
        generate(pontos);
    }

    private List<Vector2> generatePoints()
    {
        List<Vector2> paresOrdenados = new List<Vector2>();
        for (int i = 0; i < total; i++)
        {
            float coordenadaX = Random.Range(-15, 16) * distanceBetween;
            float coordenadaY = Random.Range(-15, 16) * distanceBetween;

            if(!paresOrdenados.Contains(new Vector2(coordenadaX, coordenadaY)))
                paresOrdenados.Add(new Vector2(coordenadaX, coordenadaY));
            
            //Debug.Log("Par " + (i + 1) + ": (" + coordenadaX + ", " + coordenadaY + ")");
        }

        return paresOrdenados;
    }

    private void generate(List<Vector2> pontos)
    {
        if (container != null && objetos[0] != null)
        {
            foreach (var ponto in pontos)
            {
                Vector3 spawnPosition = new Vector3(ponto.x, ponto.y, 0f);
                if (spawnPosition != Vector3.zero)
                {
                    spawnPosition.x += Random.Range(0, variacao);
                    spawnPosition.y += Random.Range(0, variacao);
                    Instantiate(objetos[Random.Range(0, objetos.Length)], spawnPosition, Quaternion.identity, container.transform);
                }   
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
