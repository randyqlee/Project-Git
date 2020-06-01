using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroLogic : MonoBehaviour
{
    public int ID;
    public HeroAsset heroAsset;
    //public Player owner;

    public string heroName;
    public string description;
    public Sprite[] heroSprite;
    
    public HeroType heroType;

    public int health;
    public int attack;
    public int effectChance;
    public int speed;
    public int criticalStrikeRate;
    public int debuffResistRate;
    public int evasionRate;
    
    public SkillAsset skill_Chance;
    public SkillAsset skill_Cooldown;
    public SkillAsset skill_Rune;

}
