using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName = "SO/Heroes")]
public class HeroAsset : ScriptableObject
{
    public string heroName;
    public string description;
    public Sprite[] heroSprite = new Sprite[3];

    public Rarity rarity;
    public Faction faction;
    public CreatureType creatureType;

    public int[] health = new int[3];
    public int[] attack = new int[3];
    public int[] chance = new int[3];
    public int[] speed = new int[3];
    public int[] critical = new int[3];
    public int[] debuff = new int[3];
    public int[] evasion = new int[3];

    public SkillAsset[] skill_Chance = new SkillAsset[3];
    public SkillAsset[] skill_Cooldown = new SkillAsset[3];
    public SkillAsset[] skill_Rune = new SkillAsset[3];



}
