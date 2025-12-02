using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene(0); // loads scene 0 from Build Settings
    }
}
