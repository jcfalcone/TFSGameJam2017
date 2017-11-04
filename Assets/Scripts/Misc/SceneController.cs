using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : Singleton<SceneController>
{

    [SerializeField]
    private string _startScene;

    // [SerializeField]
    // private string _creditsScene;

    // protected SceneController() { }

    private int _currentScene;

    public int currentScene
    {
        get { return _currentScene; }
    }

    private int lastScene = 0;

    // public enum SceneTypes { Start = 0, Options, Credits }

    // private SceneTypes _currentSceneType;

    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    // static void OnRuntimeMethodLoad()
    // {
    //     // Add the delegate to be called when the scene is loaded, between Awake and Start.
    //     SceneManager.sceneLoaded += SceneLoaded;
    // }

    // static void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    // {
    //     if (instance._currentScene > 0)
    //     {
    //         UIManager.instance.UnloadCanvas(UIManager.CanvasTypes.StartMenu);
    //     }
    // }

    void Start()
    {
        // lastScene = SceneManager.sceneCountInBuildSettings - 1;
        // UIManager.instance.LoadCanvas(UIManager.CanvasTypes.StartMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LoadPrevious();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            LoadNext();
        }

        // Debug.Log("currentScene: " + _currentScene);


    }

    public void LoadPrevious()
    {
        SetCurrentSceneIndex();
        if (_currentScene > 0)
        {
            SceneManager.LoadScene(_currentScene - 1);
        }
        else
        {
            Debug.Log("Already on first scene");
        }
    }

    public void LoadNext()
    {
        SetCurrentSceneIndex();
        if (_currentScene < lastScene)
        {
            SceneManager.LoadScene(_currentScene + 1);
        }
        else
        {
            Debug.Log("Already on last scene");
        }
    }

    public void Reload()
    {
        SetCurrentSceneIndex();
        SceneManager.LoadScene(_currentScene);
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

    // public void ShowCredits()
    // {
    //     if (_creditsScene != "")
    //     {
    //         SceneManager.LoadScene(_creditsScene);
    //     }
    //     else
    //     {
    //         Debug.LogError("Credits scene not configured");
    //     }
    // }

    // public void RestartGame()
    // {
    //     SceneManager.LoadScene(1); // assume 0 is the welcome screen
    // }

    // public void GameOver()
    // {
    //     SceneManager.LoadScene(lastScene); //assume GameOver is the last scene
    // }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetCurrentSceneIndex()
    {
        _currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // public void SetCurrentSceneType()
    // {
    //     if (_currentScene == 0)
    //     {
    //         _currentSceneType = SceneTypes.Start;
    //     }
    //     else if (SceneManager.GetSceneAt(_currentScene).name == "Options")
    //     {
    //         _currentSceneType = SceneTypes.Options;
    //     }
    //     else if (SceneManager.GetSceneAt(_currentScene).name == "Credits")
    //     {
    //         _currentSceneType = SceneTypes.Credits;
    //     }
    // }

    // void SetPreviousSceneType()
    // {
    //     SetSceneType(_previousSceneType);
    // }

    // void SetSceneType(SceneTypes sceneType)
    // {

    // }

    // new void OnDestroy()
    // {
    //     // Remove the delegate when the object is destroyed
    //     // SceneManager.sceneLoaded -= SceneLoaded;
    // }

    // public SceneTypes currentSceneType
    // {
    //     set { SetCurrentSceneType(); }
    //     get { return _currentSceneType; }
    // }
}