using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp = 100;
    public int hp;


[Header("UI Panels")]
public GameObject alwaysShowing;

    [Header("Heal SFX")]
public AudioSource healSource;   // audio source on the player (or elsewhere)
public AudioClip healClip;       // the sound to play when healing


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
    public Vector3 startPos;

void Start()
{
    startPos = transform.position;   // remember spawn
}

public void ResetHealthAndPosition()
{
    hp = maxHp;
    transform.position = startPos;
    OnHealthChanged?.Invoke(hp, maxHp);   // update health bar
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
    // disable player movement
    var move = GetComponent<PlayerController>();
    if (move != null) move.enabled = false;

    var shoot = GetComponent<PlayerShooting>();
    if (shoot != null) shoot.enabled = false;

    // hide always showing stats panel
    if (alwaysShowing != null)
        alwaysShowing.SetActive(false);

    // show death panel
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

    int oldHp = hp;

    hp = Mathf.Clamp(hp + amount, 0, maxHp);

    // play heal sound ONLY if hp actually increased
    if (hp > oldHp && healSource != null)
    {
        if (healClip != null)
            healSource.PlayOneShot(healClip);
        else
            healSource.Play();  // if the clip is already assigned on the AudioSource
    }

    OnHealthChanged?.Invoke(hp, maxHp); // update health bar
}

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}