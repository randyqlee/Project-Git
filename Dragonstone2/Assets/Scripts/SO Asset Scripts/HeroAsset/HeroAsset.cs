using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
	Common,
	Rare,
	Epic,
	Legendary
}


public class HeroAsset : ScriptableObject
{
	public string heroName;
	public Sprite image;
	
	public int maxHealth;
	public int attack;
	public int defense;
	public float chance;

	public Rarity rarity;

	[Header("Abilities - must be typed correctly")]

	public List<AbilityAsset> abilityAsset;


}
