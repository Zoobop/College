using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerStats : CharacterStats, ILoader<CharacterStats, PlayerStatDATA>
{
    #region Player Specific Stats

    [Serializable]
    public struct Experience
    {
        public int totalXP;
        public int nextLvlXP;
        public int currentXP;
        public int level;
    }

    [SerializeField] private Experience experience = new Experience { totalXP = 0, nextLvlXP = 50, currentXP = 0, level = 1 };

    public Experience XPStats { get { return experience; } private set { experience = value; } }

    public event Action<float, float> OnXPChanged = delegate { };
    public event Action<int> OnLevelUp = delegate { };

    #endregion

    #region Level Calculation

    /** Level and XP Functions
     *
     *  Functions that help automatically calculate the level of the player
     *  when loading or leveling up.
     * 
     */
    public void ComputeXP(int xpGain)
    {
        if (xpGain == 0)
            return;
        experience.totalXP += xpGain;
        experience.level = RecursiveLevelUp(xpGain);
        OnXPChanged?.Invoke(experience.currentXP, experience.nextLvlXP);
    }
    private int RecursiveLevelUp(int xpGain)
    {
        int totalCurrent = experience.currentXP + xpGain;

        // Check if xpGain is less than NextLvlXP -- if true, add xpGain to currentXp and return Level
        if (totalCurrent < experience.nextLvlXP)
        {
            experience.currentXP += xpGain;
            // Debug.Log(Level + " : " + CurrentXP + "/" + NextLvlXP);
            return experience.level;
        }
        else if (xpGain == experience.nextLvlXP)
        {
            experience.currentXP = 0;
            experience.level++;

            OnLevelUp?.Invoke(experience.level);

            LevelAlgorithm();
            // Debug.Log(Level + " : " + CurrentXP + "/" + NextLvlXP);
            return experience.level;
        }

        // Level up -- increment level, find xp needed for next level, and recursive call
        experience.level++;
        OnLevelUp?.Invoke(experience.level);
        int newXP = xpGain - experience.nextLvlXP;
        LevelAlgorithm();

        return RecursiveLevelUp(newXP);
    }
    private void LevelAlgorithm()
    {
        // Starting XP
        int startXPCap = 300;

        // Algorithm
        float levelSync = experience.level - 1;

        float algorithm = Mathf.Rad2Deg * 2.37f + ((experience.level * experience.level) - experience.level + experience.level) * (.5f * experience.level % 100);

        float algorithmResult = levelSync * algorithm + startXPCap;

        // Final
        experience.nextLvlXP = Mathf.RoundToInt(algorithmResult);
    }

    #endregion

    #region ILoader
    public void Convert(CharacterStats baseClass)
    {
        HealthStats = baseClass.HealthStats;
        StaminaStats = baseClass.StaminaStats;
    }
    
    public void SetMemberValues(PlayerStatDATA dataClass)
    {
        // Base class members
        HealthStats = dataClass.health;
        StaminaStats = dataClass.stamina;

        // Class specific
        vitality = dataClass.vitality;
        endurance = dataClass.endurance;
        strength = dataClass.strength;
        dexterity = dataClass.dexterity;
        resistance = dataClass.resistance;
    }

    public PlayerStatDATA ToData() => new PlayerStatDATA { health = HealthStats, stamina = StaminaStats, vitality = vitality, endurance = endurance, strength = strength, dexterity = dexterity, resistance = resistance };

    #endregion

    // Custom Editor Tools
    public override void SetMemberValues(EntityData.StatsInfo statsInfo)
    {
        health = statsInfo.health;
        stamina = statsInfo.stamina;
        adrenaline = statsInfo.adrenaline;
        experience = statsInfo.experience;

        vitality = statsInfo.vitality;
        endurance = statsInfo.endurance;
        strength = statsInfo.strength;
        dexterity = statsInfo.dexterity;
        resistance = statsInfo.resistance;
        divinity = statsInfo.divinity;
    }
}

[System.Serializable]
public class PlayerStatDATA : CharacterStatLoadData
{
    public Stat vitality;
    public Stat endurance;
    public Stat strength;
    public Stat dexterity;
    public Stat resistance;
}