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

	//used in CheckTauntDefender
	public bool canTargetHero;

	void Awake ()
	{
		GameManager.Instance.e_NextTurn += GameManagerNextTurn;
		
	}

	public virtual void UseAbility ()
	{

	}

	public virtual void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		//check Taunt or Defender Flag

		// if (abilityBuffs != null)
		// {
		// 	foreach (AbilityBuffs abilityBuff in abilityBuffs)
		// 	{
		// 		GameManager.Instance.AddBuffComponent(abilityBuff.buff.ToString(),abilityBuff.duration,attacker,defender);

		// 	}
		// }

		// if (abilityDebuffs != null)
		// {
		// 	foreach (AbilityDebuffs abilityDebuffs in abilityDebuffs)
		// 	{
		// 		GameManager.Instance.AddDebuffComponent(abilityDebuffs.debuff.ToString(),abilityDebuffs.duration,attacker,defender);

		// 	}
		// }

		//if No Defender or Target has Defender

		CheckTauntAndDefender(attacker, defender);

		if(canTargetHero){
			if (abilityBuffs != null)
			{
				foreach (AbilityBuffs abilityBuff in abilityBuffs)
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

			ResetCooldown();

		}else {}

	}//USeAbility


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

		ResetCooldown();

	
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
		// remainingCooldown = abilityCooldown;
		// canUseAbility = false;

		// //update Button UI
		// gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();

		
		if(canTargetHero){
			remainingCooldown = abilityCooldown;
			canUseAbility = false;

			//update Button UI
			gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();
		} else {}

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

	}//GameManagerNext Turn

	public void CheckTauntAndDefender(HeroManager attacker, HeroManager defender){

		//Check 3 states: 1) No Defender 2) Target has Defender 3) If you're target is an Ally
		if (GameManager.Instance.NoDefender(defender.GetComponentInParent<Player>())|| defender.hasDefender|| defender.GetComponentInParent<Player>().tag == attacker.GetComponentInParent<Player>().tag ){

			canTargetHero = true;

		 } else {

			 canTargetHero = false;
			 Debug.Log ("Invalid Target: Attack Defender Only");
		 }


	}//Check Taunt and Defender
}
