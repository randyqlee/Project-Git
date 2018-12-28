using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour {

	public Type skillType;
	public int abilityCooldown;

	public int remainingCooldown;

	public bool canUseAbility;
	//public bool isPassive;

	public Target target;

	public List<AbilityBuffs> abilityBuffs;
	public List<AbilityDebuffs> abilityDebuffs;

	void Awake ()
	{
		GameManager.Instance.e_NextTurn += GameManagerNextTurn;
		
	}

	public virtual void UseAbility ()
	{
		ResetCooldown();
		EndTurnCheck();
	}

	public virtual void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		GameManager.Instance.CheckTaunt(attacker, defender);

		if(GameManager.Instance.canTargetHero){
			
			if (abilityBuffs != null)
			{
				foreach (AbilityBuffs abilityBuff in abilityBuffs)
				{
					GameManager.Instance.AddBuffComponent(abilityBuff.buff.ToString(),abilityBuff.duration,attacker,defender);
					Debug.Log("ABILITY BUFF SUCCESS");

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
			EndTurnCheck();

		}//canTargetHero = True

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

		ResetCooldown();
		EndTurnCheck();

	}
	public bool CanUseAbility()
	{	
		canUseAbility = false;
		if (remainingCooldown == 0)
		{
			canUseAbility = true;
			//ResetCooldown();
			//Debug.Log ("Can use ability");
		}
		else Debug.Log ("Can't use ability"); 	
		return canUseAbility;
	}

	public void ResetCooldown()
	{
		
		remainingCooldown = abilityCooldown;
		canUseAbility = false;

		//update Button UI
		gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();		
		

	#region End Turn incorporated	
		// if(!GameManager.Instance.extraTurn){
		// 	remainingCooldown = abilityCooldown;
		// 	canUseAbility = false;

		// 	//update Button UI
		// 	gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();

		// 	GameManager.Instance.EndTurn();

		// }else {
		// 	GameManager.Instance.extraTurn = false;
		// 	GameManager.Instance.isTurnPaused = false;
			
		// }
	#endregion
		

	}//Reset Cooldown

	public virtual void GameManagerNextTurn()
	{

		

		if (remainingCooldown > 0 && gameObject.GetComponentInParent<Player>().isActive)
		{
			remainingCooldown -= 1;

			//update Button UI
			gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();
		}
	}//GameManager Next Turn

	public virtual void EndTurnCheck(){
		if(!GameManager.Instance.isTurnPaused){			

			GameManager.Instance.extraTurn = false;
			GameManager.Instance.EndTurn();

		} else {
			
			GameManager.Instance.isTurnPaused = false;
			
		}
	}//End Turn Check

	public virtual void PassiveSkillInitialization(){
		
		//Disable Access to skill
		GetComponent<Button>().interactable = false;
		GetComponentInChildren<Text>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
		
	}	






}//Ability Class
