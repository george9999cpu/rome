using UnityEngine;

public class MissionPanelsManager : MonoBehaviour
{
    [Header("Panels in order")]
    public GameObject[] panels;

    [Header("HUD Group (always showing UI)")]
    public GameObject hudGroup;

    private int index = 0;

    void Start()
    {
        BeginIntro();   // auto start when scene loads
    }

    public void BeginIntro()
    {
        Time.timeScale = 0f;   // pause game

        if (hudGroup != null)
            hudGroup.SetActive(false);   // hide HUD during intro

        index = 0;             // always start at first panel
        ShowPanel(index);
    }

    public void NextPanel()
    {
        index++;

        if (index >= panels.Length)
        {
            CloseAll();
            return;
        }

        ShowPanel(index);
    }

    private void ShowPanel(int i)
    {
        foreach (GameObject p in panels)
            if (p != null)
                p.SetActive(false);

        if (panels[i] != null)
            panels[i].SetActive(true);
    }

    private void CloseAll()
    {
        foreach (GameObject p in panels)
            if (p != null)
                p.SetActive(false);

        if (hudGroup != null)
            hudGroup.SetActive(true);   // show HUD again

        Time.timeScale = 1f;   // unpause game
        index = 0;
    }
}
