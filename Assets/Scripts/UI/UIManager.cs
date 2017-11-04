using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public enum CanvasTypes { StartMenu, CheckQuit, Level, PauseMenu }


    //     [SerializeField]
    //     private GameObject _optionsScreenCanvas;
    //     [SerializeField]
    //     private GameObject _creditsScreenCanvas;
    //     [SerializeField]
    //     private GameObject _pauseCanvas;

    [SerializeField]
    private GameObject _startScreenCanvas;
    private GameObject _btnStart;
    private GameObject _btnQuit;

    [SerializeField]
    private GameObject _quitConfirmationCanvas;

    private GameObject _btnSure;
    private GameObject _btnNevermind;

    [SerializeField]
    private string _startScene;

    void Update()
    {
        if (SceneController.instance.currentScene > 0 && _startScreenCanvas.activeInHierarchy == true)
        {
            UnloadCanvas(UIManager.CanvasTypes.StartMenu);
        }
    }

    public void LoadCanvas(CanvasTypes canvasType)
    {
        if (canvasType == CanvasTypes.StartMenu)
        {
            LoadStartMenuCanvas();
        }
        else if (canvasType == CanvasTypes.CheckQuit)
        {
            LoadCheckQuitCanvas();
        }
    }

    public void UnloadCanvas(CanvasTypes canvasType)
    {
        if (canvasType == CanvasTypes.StartMenu)
        {
            UnloadStartMenuCanvas();
        }
        else if (canvasType == CanvasTypes.CheckQuit)
        {
            UnloadCheckQuitCanvas();
        }
    }

    void LoadStartMenuCanvas()
    {
        _startScreenCanvas.SetActive(true);

        _btnStart = GameObject.Find("btnStart");
        if (_btnStart)
        {
            _btnStart.GetComponent<Button>().onClick.AddListener(SceneController.instance.StartGame);
        }

        // _btnQuit = GameObject.Find("btnCredits");
        // if (_btnQuit)
        // {
        //     _btnStart.GetComponent<Button>().onClick.AddListener(SceneController.instance.ShowCredits);
        // }

        _btnQuit = GameObject.Find("btnQuit");
        if (_btnQuit)
        {
            _btnQuit.GetComponent<Button>().onClick.AddListener(SceneController.instance.QuitGame);
        }
    }

    void UnloadStartMenuCanvas()
    {
        Debug.Log("here");
        _startScreenCanvas.SetActive(false);
    }

    public void LoadCheckQuitCanvas()
    {
        _quitConfirmationCanvas.SetActive(true);

        _btnSure = GameObject.Find("btnSure");
        if (_btnSure)
        {
            _btnSure.GetComponent<Button>().onClick.AddListener(SceneController.instance.QuitGame);
        }

        _btnNevermind = GameObject.Find("btnNevermind");
        if (_btnNevermind)
        {
            _btnNevermind.GetComponent<Button>().onClick.AddListener(UnloadCheckQuitCanvas);
        }
    }

    public void UnloadCheckQuitCanvas()
    {
        _quitConfirmationCanvas.SetActive(false);
    }

    public void StartGame()
    {
        if (_startScene != "")
        {
            SceneManager.LoadScene(_startScene);
        }
        else
        {
            Debug.LogError("First level not configured");
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
