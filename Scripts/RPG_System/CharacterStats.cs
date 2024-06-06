using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxStamina = 100;

    public int currentHealth { get; private set; }
    public float currentStamina {  get; private set; }

    public bool blockHealth = false;
    public bool blockStamina = false;

    public Stat damage;
    public Stat armor;

    public event System.Action<int, int> OnHealthChanged;
    public event System.Action<float,float> OnStaminaChanged;
    public event System.Action OnGetHit;

    private void Awake()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (currentStamina < maxStamina)
        {
            if (currentStamina > 20)
                blockStamina = false;

            currentStamina += 0.08f;

            if (OnStaminaChanged != null)
            {
                OnStaminaChanged(maxStamina, currentStamina);
            }
        }
    }

    public void MinusStamina(int stamina)
    {
        stamina += armor.GetValue();
        stamina = Mathf.Clamp(stamina, 0, int.MaxValue);

        currentStamina -= stamina;
        Debug.Log(transform.name + " minus " + stamina + " stamina.");

        if (OnStaminaChanged != null)
        {
            OnStaminaChanged(maxStamina, currentStamina);
        }

        if (currentStamina <= 0)
        {
            currentStamina = 0;

            blockStamina = true;
            Debug.Log("Вы устали");
        }
    }

    public void TakeDamage(int damage)
    {
        //if (blockHealth) //ShieldDamagRezist
           //damage -= 2;

        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if(OnGetHit != null)
        {
            OnGetHit.Invoke();
        }

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
