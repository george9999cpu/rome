using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp = 100;
    public int hp;

    [Range(0f, 1f)] public float armorPercent = 0.7f;
    public GameObject deathPanel;
    public string restartSceneName = "SampleScene";
    public string menuSceneName = "MainMenu";

    // This is what the health bar listens to
    public static event Action<int, int> OnHealthChanged;

    private void Awake()
    {
        hp = maxHp;
    }

    private void OnEnable()
    {
        OnHealthChanged?.Invoke(hp, maxHp); // initial value for UI
    }

    public void TakeDamage(int dmg)
    {
        int finalDmg = Mathf.Max(1, Mathf.RoundToInt(dmg * (1f - armorPercent)));
        hp = Mathf.Clamp(hp - finalDmg, 0, maxHp);

        OnHealthChanged?.Invoke(hp, maxHp); // ← this updates the bar

        if (hp <= 0)
            Die();
    }

       private void Update()
{
    if (Input.GetKeyDown(KeyCode.H))
        TakeDamage(10);
}

    void Die()
    {
        // Fixed lines that were causing errors
        var move = GetComponent<PlayerController>();
        if (move != null) move.enabled = false;

        var shoot = GetComponent<PlayerShooting>();
        if (shoot != null) shoot.enabled = false;

        if (deathPanel != null)
            deathPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    // FIXED: No more ternary type mismatch error
    public void RestartGame()
    {
        Time.timeScale = 1f;
        if (string.IsNullOrEmpty(restartSceneName))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(restartSceneName);
        }
    }
public void AddHealth(int amount)
{
    if (hp <= 0) return; // already dead, don’t heal

    hp = Mathf.Clamp(hp + amount, 0, maxHp);

    OnHealthChanged?.Invoke(hp, maxHp); // update health bar
}
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}