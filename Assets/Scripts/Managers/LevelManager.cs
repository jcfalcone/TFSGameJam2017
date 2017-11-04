using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    bool isPaused;
    [SerializeField] GameObject pauseCanvas;

    [SerializeField] Portal portal;
    [SerializeField] int pumpkins;
    [SerializeField] SoundManager.AudioClips levelAudio;
    
	void Awake ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        SoundManager.instance.Play(this.levelAudio);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape))
        {
            if(Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
                isPaused = false;
                pauseCanvas.SetActive(false);
            }
            else
            {
                Time.timeScale = 0.0f;
                isPaused = true;
                pauseCanvas.SetActive(true);
            }
        }
    }

    public void LightUpPumpikin()
    {
        this.pumpkins--;

        if(this.pumpkins == 0)
        {
            portal.ActivatePortal();
            Destroy(gameObject);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool GetIsPaused() { return isPaused; }
}
