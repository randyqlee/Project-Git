using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour {

	public int abilityCooldown;

	public int remainingCooldown;

	public bool canUseAbility;

	public Target target;

	public List<AbilityBuffs> abilityBuffs;
	public List<AbilityDebuffs> abilityDebuffs;

<<<<<<< HEAD
	//used in CheckTauntDefender
	//public bool canTargetHero;

=======
>>>>>>> parent of cf13af5... Defender and Taunt Upgrade
	void Awake ()
	{
		GameManager.Instance.e_NextTurn += GameManagerNextTurn;
		
	}

	public virtual void UseAbility ()
	{

	}

	public virtual void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
<<<<<<< HEAD
		GameManager.Instance.CheckTauntAndDefender(attacker, defender);

		if(GameManager.Instance.canTargetHero){
			if (abilityBuffs != null)
=======

		if (abilityBuffs != null)
		{
			foreach (AbilityBuffs abilityBuff in abilityBuffs)
>>>>>>> parent of cf13af5... Defender and Taunt Upgrade
			{
				GameManager.Instance.AddBuffComponent(abilityBuff.buff.ToString(),abilityBuff.duration,attacker,defender);

			}
		}

		if (abilityDebuffs != null)
		{
			foreach (AbilityDebuffs abilityDebuffs in abilityDebuffs)
			{
				GameManager.Instance.AddDebuffComponent(abilityDebuffs.debuff.ToString(),abilityDebuffs.duration,attacker,defender);

			}
		}

	}


	public virtual void UseAbilityRandom (HeroManager attacker, HeroManager defender, int targetCount)
	{

		if (abilityBuffs != null)
		{
			foreach (AbilityBuffs abilityBuff in abilityBuffs)
			{
				GameManager.Instance.AddBuffComponentRandom(abilityBuff.buff.ToString(),abilityBuff.duration,attacker,defender, targetCount);

			}
		}

		if (abilityDebuffs != null)
		{
			foreach (AbilityDebuffs abilityDebuffs in abilityDebuffs)
			{
				GameManager.Instance.AddDebuffComponentRandom(abilityDebuffs.debuff.ToString(),abilityDebuffs.duration,attacker,defender,targetCount);

			}
		}

	}
	public bool CanUseAbility()
	{	
		canUseAbility = false;
		if (remainingCooldown == 0)
		{
			canUseAbility = true;
			//ResetCooldown();
			Debug.Log ("Can use ability");
		}
		else Debug.Log ("Can't use ability"); 	
		return canUseAbility;



	}

	public void ResetCooldown()
	{
		remainingCooldown = abilityCooldown;
		canUseAbility = false;

		//update Button UI
<<<<<<< HEAD
		gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();	
=======
		gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();
>>>>>>> parent of cf13af5... Defender and Taunt Upgrade

	}

	public virtual void GameManagerNextTurn()
	{


		Debug.Log ("Cooldown of " + name + " : " + remainingCooldown);

		if (remainingCooldown > 0 && gameObject.GetComponentInParent<Player>().isActive)
		{
			remainingCooldown -= 1;

			//update Button UI
			gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();
		}

<<<<<<< HEAD
	}//GameManagerNext Turn

	// public void CheckTauntAndDefender(HeroManager attacker, HeroManager defender){

	// 	//Check 3 states: 1) No Defender 2) Target has Defender 3) If you're target is an Ally
	// 	if (GameManager.Instance.NoDefender(defender.GetComponentInParent<Player>())|| defender.hasDefender|| defender.GetComponentInParent<Player>().tag == attacker.GetComponentInParent<Player>().tag ){

	// 		canTargetHero = true;

	// 	 } else {

	// 		 canTargetHero = false;
	// 		 Debug.Log ("Invalid Target: Attack Defender Only");
	// 	 }


	// }//Check Taunt and Defender
=======
	}
>>>>>>> parent of cf13af5... Defender and Taunt Upgrade
}
