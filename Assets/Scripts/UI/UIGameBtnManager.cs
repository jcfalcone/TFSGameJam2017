using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameBtnManager : MonoBehaviour {

    // Use this for initialization

    public string MenuScene;

    public GameObject UICanvas;
    public GameObject Settings;
    public GameObject AudioSettings;
    public GameObject CheckBackMenu;
    public GameObject SoundEffectBar;

    void Start () {

        //Initialize UI Activities.
        UICanvas.SetActive(true);
        Settings.SetActive(true); // <--- This has to be set to fales when the in game control finished.
        AudioSettings.SetActive(false);
        CheckBackMenu.SetActive(false);

        if(MenuScene == null)
        {
            Debug.Log("<SceneManager>: Back to Menu button target scene not set.");
        }
	}

    private void Update()
    {
        
    }

    public void ResumeBtn()
    {
        Settings.SetActive(false);
    }

    public void AudioBtn()
    {
        AudioSettings.SetActive(true);
    }

    public void AudioBackSetting ()
    {
        AudioSettings.SetActive(false);
    }

    public void BackMenuBtn()
    {
        CheckBackMenu.SetActive(true);
    }

    public void BackMenuConfrim()
    {
        SceneManager.LoadScene(MenuScene);
    }

    public void BackMenuCancel()
    {
        CheckBackMenu.SetActive(false);
    }

}
