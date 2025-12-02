using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManage : MonoBehaviour
{
    public string gameSceneName = "SampleScene";   // change to your scene name

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}