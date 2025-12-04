
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject optPanel;   

public void ShowOptions()
{
    if (optPanel != null)
        optPanel.SetActive(true);
}



    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        AudioManager.inst.OnStartGame();

       
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
