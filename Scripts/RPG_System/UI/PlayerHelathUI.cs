using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class PlayerHelathUI : MonoBehaviour
{
    private PlayerStats stats;
    [SerializeField] Image healthSlider;
    [SerializeField] Image staminaSlider;
    [SerializeField] Image experienceSlider;

    [SerializeField] Text levelText;

    public delegate void PlayerLevelEventHandler();
    public static event PlayerLevelEventHandler OnPlayerLevelChange;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }
    private void Start()
    {
        OnPlayerLevelChange += UpdateLevel;

        stats.OnHealthChanged += OnHealthChangedPlayer;
        stats.OnStaminaChanged += OnStaminaChangedPlayer;
    }
    public static void PlayerLeveleChanged()
    {
        OnPlayerLevelChange();
    }

    private void OnHealthChangedPlayer(int maxHealth, int currentHealth)
    {
        float healthPercent = currentHealth / (float)maxHealth;
        healthSlider.fillAmount = healthPercent;
    }
    private void OnStaminaChangedPlayer(float maxStamina, float currentStamina)
    {
        float staminaPercent = currentStamina / (float)maxStamina;
        staminaSlider.fillAmount = staminaPercent;
    }
    private void UpdateLevel()
    {
        levelText.text = stats.playerLevel.Level.ToString();
        experienceSlider.fillAmount=stats.playerLevel.CurrentExperience / (float)stats.playerLevel.RequiredExperience;
    }

}
