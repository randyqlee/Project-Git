using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Target
{
	Enemies,
	Allies,
	Any,
	

}

public enum Type
{
	Active,
	Passive,
	Silenced,
	Stunned,
	ExtraTurn
}


[System.Serializable]
public class AbilityAsset2 {

	public string abilityName;
	//public Ability abilityScript;
	public Type skillType;
	public Sprite icon;

	[TextArea(5,10)]
	public string description;

	[Header("AbilityInfo - must be typed correctly")]
	public string abilityEffect;

	public int abilityCoolDown;

	public Target target;


	[Header("Ability Buffs")]
	public List<AbilityBuffs> abilityBuffs;

	//since Bufflist buff is an enum, calling method should use enum.GetName() to convert the enum to its string text, so we can use gameObject.AddComponent(System.Type.GetType(buffName));



	[Header("Ability Debuffs")]
	public List<AbilityDebuffs> abilityDebuffs;

}
