using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityGeneralInfo
{
    #region Enums

    public enum Species
    {
        Unknown,
        Human,
        Vhas,
        Maziini
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum Team
    {
        Neutral,
        Ally,
        Enemy
    }

    public enum Faction
    {
        Survivors,
        Despaired,
        Accursed,
        Servants,
        Overlords
    }

    public enum EntityType
    {
        Mortal,
        Abnormal,
        Wraith,
        Divine
    }

    #endregion

    #region Entity Names

    private readonly List<string> MaleNames = new List<string>()
    {
        "Anders",
        "Dren",
        "Faris",
        "Karth",
        "Darius",
        "Leo",
        "Carmin",
        "Ja'Farr",
        "Beodin",
        "Jarin",
        "Cannen",
        "Lynis",
        "Justinian",
        "Ghaul",
        "Terrice",
        "Kalo",
        "Gideon",
        "Jol'Gin",
        "Erwin",
        "Paknum",
        "Jose",
        "Barreth",
        "Corwin",
        "Hun'Olen",
        "Felix",
        "Ledren",
        "Quo Kan",
        "Hamlin",
        "Frezto",
        "Pontin",
        "Yumian",
        "Lupin"
    };

    private readonly List<string> FemaleNames = new List<string>()
    {
        "Dana",
        "Janus",
        "Scylla",
        "Cynthia",
        "Dermini",
        "Trinity",
        "Ruby",
        "Azula",
        "Sandra",
        "Kassandra",
        "Elain",
        "Helena"
    };

    private readonly List<string> WraithNames = new List<string>()
    {
        "Uun'Gen",
        "Nin'Gennai",
        "Zhee'Hunn",
        "Raq'Zhon",
        "Bhaj'Lue"
    };

    #endregion

    #region Fields and Properties

    public string name;
    public Species species;
    public Gender gender;

    public Team team;
    public Faction faction;
    public EntityType entityType;

    public GameObject entityMeshObject;

    private readonly Color Neutral = new Color(20f, 20f, 20f);
    private readonly Color Ally = new Color(0f, 14f, 191f);
    private readonly Color Enemy = new Color(191f, 14f, 0f);

    #endregion

    public EntityGeneralInfo() => Reset();

    #region Modification

    private void Reset()
    {
        name = "New Entity";
        species = Species.Unknown;
        gender = Gender.Male;

        team = Team.Neutral;
        faction = Faction.Survivors;
        entityType = EntityType.Mortal;
    }

    #endregion

    #region Randomization

    public void RandomizeAll()
    {
        RandomizeEntityType();
        RandomizeGender();
        RandomizeName();
        RandomizeEntityBackground();
    }

    private void RandomizeEntityType()
    {
        entityType = (EntityType)Random.Range(0, 5);
    }

    private void RandomizeName()
    {
        if (gender == Gender.Male)
        {
            var random = Random.Range(0, MaleNames.Count);

            name = MaleNames[random];
        }
        else if (gender == Gender.Female)
        {
            var random = Random.Range(0, FemaleNames.Count);

            name = FemaleNames[random];
        }
    }

    private void RandomizeGender()
    {
        gender = (Gender)Random.Range(0, 2);
    }

    private void RandomizeEntityBackground()
    {
        switch (entityType)
        {
            default:
                species = (Species)Random.Range(1, 4);
                team = (Team)Random.Range(0, 3);
                faction = (Faction)Random.Range(0, 2);
                return;

            case EntityType.Abnormal:
                species = (Species)Random.Range(1, 4);
                team = Team.Enemy;
                faction = Faction.Accursed;
                return;

            case EntityType.Wraith:
                species = Species.Unknown;
                team = Team.Enemy;
                faction = Faction.Servants;
                return;

            case EntityType.Divine:
                species = Species.Unknown;
                team = Team.Enemy;
                faction = Faction.Overlords;
                return;
        }
    }

    #endregion

    #region Misc Functions

    public Color GetTeamColor()
    {
        Color shaderColor;

        switch (team)
        {
            default:
                shaderColor = Neutral;
                break;
            case Team.Ally:
                shaderColor = Ally;
                break;
            case Team.Enemy:
                shaderColor = Enemy;
                break;
        }

        return shaderColor;
    }

    #endregion
}
