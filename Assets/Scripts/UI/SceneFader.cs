using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class SceneFader : Singleton<SceneFader>
{
    public float fadeSpeed = 0.8f;
    public enum FadeDirection
    {
        In,
        Out
    }


    public IEnumerator Fade(FadeDirection fadeDirection, Image transitionCanvas)
    {
        float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;
        if (fadeDirection == FadeDirection.Out)
        {
            while (alpha >= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection, transitionCanvas);
                yield return null;
            }
            transitionCanvas.gameObject.SetActive(false);
        }
        else
        {
            transitionCanvas.gameObject.SetActive(true);
            while (alpha <= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection, transitionCanvas);
                yield return null;
            }
        }
    }

    public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad, Image transitionCanvas)
    {
        yield return Fade(fadeDirection, transitionCanvas);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress <= 0.89f)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
        //SceneManager.LoadSceneAsync(sceneToLoad);
    }

    private void SetColorImage(ref float alpha, FadeDirection fadeDirection, Image transitionCanvas)
    {
        transitionCanvas.color = new Color(transitionCanvas.color.r, transitionCanvas.color.g, transitionCanvas.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
    }
}