using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject portal;
    [SerializeField] string nextLevelName;

    public void ActivatePortal()
    {
        portal.SetActive(true);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(nextLevelName);
    }
}
