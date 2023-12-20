using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmicDustController : MonoBehaviour
{
    public HUDController HUDController;
    public Transform position;
    public GameObject bg;
    public Animator[] sprites;
    private bool enable = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            Vector3 novaPosicao = new Vector3(position.localPosition.x, position.localPosition.y, 0);
            transform.localPosition = novaPosicao;
        }
    }

    public void enableCosmicDust()
    {
        enable = true;
        gameObject.SetActive(true);
        bg.GetComponent<Animator>().SetBool("isEnable", true);
        animeSprite(true);
        HUDController.visibilidade(false);
    }

    public void disableCosmicDust()
    {
        if(gameObject.activeSelf)
            StartCoroutine(desable());
    }

    private void animeSprite(bool enable)
    {
        foreach(Animator anim in sprites)
        {
            if(enable)
                anim.SetBool("isEnable", true);
            else
                anim.SetBool("isEnable", false);
        }
    }

    IEnumerator desable()
    {
        bg.GetComponent<Animator>().SetBool("isEnable", false);
        animeSprite(false);
        yield return new WaitForSeconds(.4f);
        enable = false;
        gameObject.SetActive(false);
        HUDController.visibilidade(true);
    }
}
