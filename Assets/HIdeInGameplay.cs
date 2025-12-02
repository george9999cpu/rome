using UnityEngine;
using UnityEngine.SceneManagement;

public class HIdeInGameplay : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            gameObject.SetActive(false);
        }
    }
}