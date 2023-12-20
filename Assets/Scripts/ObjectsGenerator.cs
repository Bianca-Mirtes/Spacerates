using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectsGenerator: MonoBehaviour
{
    public GameObject container;
    public GameObject[] objetos;
    public int min = 0;
    public int max = 0;
    public int total = 70;
    public int distanceBetween = 2;
    public int tamanhoMaximo;
    private float variacao = 0.95f;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector2> pontos = generatePoints();
        generate(pontos);
    }

    private List<Vector2> generatePoints()
    {
        int tamanho = 0;
        while (tamanho < tamanhoMaximo) { tamanho += distanceBetween; }

        List<Vector2> paresOrdenados = new List<Vector2>();
        if(min > 0 && max > 0)
        {
            total = Random.Range(min, max + 1);
        }
        for (int i = 0; i < total; i++)
        {
            float coordenadaX = Random.Range(-1 * tamanho, tamanho + 1) * distanceBetween;
            float coordenadaY = Random.Range(-1 * tamanho, tamanho + 1) * distanceBetween;

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
