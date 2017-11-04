using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuBtnManager : MonoBehaviour
{

    public string StartTargetScene;

    public GameObject UICanvas;
    public GameObject Settings;
    public GameObject Credits;
    public GameObject CheckQuit;

    // Use this for initialization
    void Start()
    {
        //Initialize Menu Activities.
        UICanvas.SetActive(true);
        CheckQuit.SetActive(false);

        //Debug Targeting Scenes.
        if (StartTargetScene == null)
        {
            Debug.Log("<SceneManager>:Start botton target scene not set.");
        }
    }

    //Function for hitting Start button.
    public void StartBtn(string TargetScene)
    {
        SceneManager.LoadScene(TargetScene);
    }

    public void SettingBtn()
    {
        Settings.SetActive(true);
    }

    public void CreditsBtn()
    {
        Credits.SetActive(true);
    }

    public void QuitBtn()
    {
        CheckQuit.SetActive(true);
    }

    public void QuitConfirm()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
    }

    public void QuitCancel()
    {
        Debug.Log("Quit Game Canceld.");
        CheckQuit.SetActive(false);
    }

}
