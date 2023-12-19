using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmicDustController : MonoBehaviour
{
    public Transform position;
    public GameObject bg;
    private bool enable = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(enable)
            transform.localPosition = position.localPosition;
    }

    public void enableCosmicDust()
    {
        enable = true;
        gameObject.SetActive(true);
        bg.GetComponent<Animator>().SetBool("isEnable", true);
    }

    public void disableCosmicDust()
    {
        if(gameObject.activeSelf)
            StartCoroutine(desable());
    }

    IEnumerator desable()
    {
        bg.GetComponent<Animator>().SetBool("isEnable", false);
        yield return new WaitForSeconds(.3f);
        enable = false;
        gameObject.SetActive(false);
    }
}
