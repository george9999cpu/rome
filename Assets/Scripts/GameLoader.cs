using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("MainMenu"); // name of your main menu scene
    }
}
