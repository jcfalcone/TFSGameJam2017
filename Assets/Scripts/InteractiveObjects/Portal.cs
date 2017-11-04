using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject portal;
    [SerializeField] BoxCollider portalCollider;
    [SerializeField] string nextLevelName;

    public void ActivatePortal()
    {
        portal.SetActive(true);
        this.portalCollider.enabled = true;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(nextLevelName);
    }
}
