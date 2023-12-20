using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Transform menuPainel;
    public Button play;
    public GameObject configPainel;
    public Slider volume;
    public TextMeshProUGUI volumeTxt;

    public Color corDeSelecao;
    public Color corDeDesabilitado;

    private GameObject btnSelected;

    public AudioClip musicTheme;
    public AudioClip moveSound;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(musicTheme);
        if(EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(play.gameObject);
        if(SceneManager.GetActiveScene().buildIndex == 0)
            configPainel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        updateSelectedBtn();
        if(volume != null)
            AudioListener.volume = volume.value;
    }
    private void soundEffect(AudioClip audio)
    {
        menuPainel.GetComponent<AudioSource>().PlayOneShot(audio);
    }

    private void updateSelectedBtn()
    {
        GameObject currentObj = EventSystem.current.currentSelectedGameObject;
        if ((btnSelected != currentObj) && currentObj != null)
        {
            soundEffect(moveSound);
            Button[] allButtons = FindObjectsOfType<Button>();
            foreach (Button button in allButtons)
            {
                TextMeshProUGUI btnText = button.GetComponentInChildren<TextMeshProUGUI>();
                btnText.color = corDeDesabilitado;
            }

            if (EventSystem.current.currentSelectedGameObject.name == "ConfigVoltar")
                volumeTxt.color = corDeDesabilitado;

            btnSelected = EventSystem.current.currentSelectedGameObject;
            TextMeshProUGUI texto = btnSelected.GetComponentInChildren<TextMeshProUGUI>();
            texto.color = corDeSelecao;
            //VFX de selecao de botao
        }
        if(currentObj == null)
            EventSystem.current.SetSelectedGameObject(btnSelected);
        
    }

    public void hover(GameObject btn){
        EventSystem.current.SetSelectedGameObject(btn);
    }

    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void loadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void exit()
    {
        Application.Quit();
    }
}
