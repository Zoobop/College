using System;
using System.Collections;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour, IStats
{
    #region Stat Structs

    [Serializable]
    public struct Health
    {
        public int currentHealth;
        public int maxHealth;
    }

    [Serializable]
    public struct Stamina
    {
        public int currentStamina;
        public int maxStamina;
    }

    [Serializable]
    public struct Adrenaline
    {
        public int currentAdrenaline;
        public int maxAdrenaline;
    }

    #endregion

    #region Fields and Properties

    [SerializeField] protected Health health = new Health { currentHealth = 100, maxHealth = 100 };
    [SerializeField] protected Stamina stamina = new Stamina { currentStamina = 80, maxStamina = 80 };
    [SerializeField] protected Adrenaline adrenaline = new Adrenaline { currentAdrenaline = 0, maxAdrenaline = 20 };

    public static string vitalityDescription = "Health";
    public static string enduranceDescription = "Stamina Amount -- Movement per turn";
    public static string strengthDescription = "Base Damage";
    public static string dexterityDescription = "Attack Speed -- 20% increase to base damage";
    public static string resistanceDescription = "Resistance to damage";
    public static string divinityDescription = "Increase damage to divine abilities";

    [SerializeField]
    protected Stat vitality = new Stat
    {
        Description = vitalityDescription,
        Level = 1,
        Value = 1
    };

    [SerializeField]
    protected Stat endurance = new Stat
    {
        Description = enduranceDescription,
        Level = 1,
        Value = 1
    };

    [SerializeField]
    protected Stat strength = new Stat
    {
        Description = strengthDescription,
        Level = 1,
        Value = 1
    };

    [SerializeField]
    protected Stat dexterity = new Stat
    {
        Description = dexterityDescription,
        Level = 1,
        Value = 1
    };

    [SerializeField]
    protected Stat resistance = new Stat
    {
        Description = resistanceDescription,
        Level = 1,
        Value = 1
    };

    [SerializeField]
    protected Stat divinity = new Stat
    {
        Description = divinityDescription,
        Level = 1,
        Value = 1
    };

    protected WaitForSeconds tickSpeed = new WaitForSeconds(.8f);

    public Stat Vitality => vitality;
    public Stat Endurance => endurance;
    public Stat Strength => strength;
    public Stat Dexterity => dexterity;
    public Stat Resistance => resistance;
    public Stat Divinity => divinity;

    public Health HealthStats { get => health; protected set => health = value; }
    public Stamina StaminaStats { get => stamina; protected set => stamina = value; }
    public Adrenaline AdrenalineStats { get => adrenaline;  protected set => adrenaline = value; }

    public bool IsDead { get; protected set; } = false;

    public event Action<float, float> OnHealthChanged = delegate { };
    public event Action<float, float> OnStaminaChanged = delegate { };
    public event Action<float, float> OnAdrenalineChanged = delegate { };

    #endregion

    #region Stat Modification

    public virtual void ModifyHealth(int change)
    {
        health.currentHealth += change;

        health.currentHealth = Mathf.Clamp(health.currentHealth, 0, health.maxHealth);

        // Check if dead
        if (health.currentHealth == 0)
            IsDead = true;

        OnHealthChanged?.Invoke(health.currentHealth, health.maxHealth);
    }

    public virtual void ModifyStamina(int change)
    {
        stamina.currentStamina += change;

        stamina.currentStamina = Mathf.Clamp(stamina.currentStamina, 0, stamina.maxStamina);
      
        OnStaminaChanged?.Invoke(stamina.currentStamina, stamina.maxStamina);
    }

    public virtual void ModifyAdrenaline(int change)
    {
        adrenaline.currentAdrenaline += change;

        adrenaline.currentAdrenaline = Mathf.Clamp(adrenaline.currentAdrenaline, 0, adrenaline.maxAdrenaline);

        OnAdrenalineChanged?.Invoke(adrenaline.currentAdrenaline, adrenaline.maxAdrenaline);
    }

    public virtual void StaminaGainPerRound()
    {
        int gain = endurance.Value / 6 + 20;
        ModifyStamina(gain);
    }

    public void IncreaseMaxHealth(int change)
    {
        if (change < 0)
            change *= -1;

        health.maxHealth += change;

        OnHealthChanged?.Invoke(health.currentHealth, health.maxHealth);
    }

    public void TickGain(int healthTick = 0, int staminaTick = 0, int adrenalineTick = 0, float tickTime = 0) => StartCoroutine(StatTick(healthTick, staminaTick, adrenalineTick, tickTime));

    public IEnumerator StatTick(int healthTick, int staminaTick, int adrenalineTick, float tickTime)
    {
        yield return tickTime;

        while (tickTime > 0)
        {
            if (healthTick != 0)
                ModifyHealth(healthTick);

            if (staminaTick != 0)
                ModifyStamina(staminaTick);

            if (adrenalineTick != 0)
                ModifyAdrenaline(adrenalineTick);

            tickTime--;
            yield return tickSpeed;
        }
    }

    #endregion

    // Custom Editor Tools
    public abstract void SetMemberValues(EntityData.StatsInfo statsInfo);
}

public abstract class CharacterStatLoadData
{
    public CharacterStats.Health health;
    public CharacterStats.Stamina stamina;
    public CharacterStats.Adrenaline adrenaline;
}