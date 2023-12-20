using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour
{
    public Transform position;
    public Transform center;
    public GameObject asteroidsContainer;
    public GameObject enemysContainer;

    public GameObject radarContainer;
    public GameObject[] pontos;
    public float scale;

    List<Transform> asteroids = new List<Transform>();
    private float bgX;
    private float bgY;
    private Vector3 centroDoCirculo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.localPosition = position.localPosition;
        bgX = center.position.x;
        bgY = center.position.y;
        centroDoCirculo = new Vector3(bgX, bgY, 0f);
    }

    void FixedUpdate()
    {
        deletePoints();

        asteroids.Clear();
        asteroids = captureChildren(asteroidsContainer);

        //asteroides
        float playerX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        float playerY = GameObject.FindGameObjectWithTag("Player").transform.position.y;
        foreach (Transform asteroid in asteroids)
        {
            if (asteroid.CompareTag("asteroid"))
            {
                float x = bgX + (asteroid.position.x - playerX) * scale;
                float y = bgY + (asteroid.position.y - playerY) * scale;
                Vector3 spawnPosition = new Vector3(x, y, 0f);
                if(inside(spawnPosition))
                    Instantiate(pontos[0], spawnPosition, Quaternion.identity, radarContainer.transform);
            }
        }
    }

    private void deletePoints()
    {
        int numFilhos = radarContainer.transform.childCount;

        for (int i = 0; i < numFilhos; i++)
        {
            Transform filho = radarContainer.transform.GetChild(i);

            Destroy(filho.gameObject);
        }
    }

    private bool inside(Vector3 pontoParaChecar)
    {
        float distancia = Vector3.Distance(centroDoCirculo, pontoParaChecar);
        if (distancia <= 1.5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private List<Transform> captureChildren(GameObject container)
    {
        List<Transform> objetosFilhos = new List<Transform>();
        container.GetComponentsInChildren<Transform>(true, objetosFilhos);
        return objetosFilhos;
    }

    public void disableRadar()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    public void enableRadar()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
}
