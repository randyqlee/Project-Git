using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hero
{
    public string name;
    public string description;
    public List<HeroAttributes> heroAttributes;
    public int heroLevel;
    public Rarity rarity;
    public Faction faction;
    public CreatureType creatureType;
}
