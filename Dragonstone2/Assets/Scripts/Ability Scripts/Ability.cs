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
	public bool cantReduce;

	public Target target;

	public List<AbilityBuffs> abilityBuffs;
	public List<AbilityDebuffs> abilityDebuffs;

	public virtual void Awake ()
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
					//Debug.Log("ABILITY BUFF SUCCESS");

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

	public virtual void UseAbilityPassive(){
		
		//Disable Access to skill UI
		GetComponent<Button>().interactable = false;
		GetComponentInChildren<Text>().enabled = false;
		//GetComponent<BoxCollider2D>().enabled = false;
		
	}	


	public bool CanUseAbility()
	{	
		canUseAbility = false;
		if (remainingCooldown == 0 && skillType == Type.Active)
		{
			canUseAbility = true;
			//ResetCooldown();
			//Debug.Log ("Can use ability");
		}
		else Debug.Log ("Can't use ability"); 	
		return canUseAbility;
	}

	//used by GameManager Next turn for cooldown reduction
	public void ResetCooldown()
	{	
	
			remainingCooldown = abilityCooldown;
			canUseAbility = false;

			//update Button UI
			gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();		

	}//Reset Cooldown


	public void ReduceCooldown(int reduction){

		if(cantReduce){
			Debug.Log("Can't reduce cooldown");			
		} else {
		
		remainingCooldown -= reduction;

		if(remainingCooldown < 0)	
		remainingCooldown=0;

		//update Button UI
		gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();
		}
	}//Reduce Cooldown

	public void RefreshCooldown (){
		if(cantReduce)
		{
			Debug.Log("Can't reduce cooldown");
		} 
		else 
		{		
		remainingCooldown = 0;
		//update Button UI
		gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();
		}

	}//RefreshCooldown

	public void MaxCooldown(){

		if(cantReduce)
		{
			Debug.Log("Can't set to Max cooldown");
		} 
		else 
		{		
		remainingCooldown = abilityCooldown;
		//update Button UI
		gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();
		}


	}//MaxCooldown

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

			//logic for hero specific extra turns
			if(GameManager.Instance.extraTurn){				
				Player player = gameObject.GetComponentInParent<Player>();
				if(player.isActive){
					HeroManager[] activeHeroes = player.GetComponentsInChildren<HeroManager>();
					foreach(HeroManager activeHero in activeHeroes){
						activeHero.tag = player.tag;
					}
				}

			}
			
			
			//no extra turn
			GameManager.Instance.extraTurn = false;
			GameManager.Instance.EndTurn();

		} else {

			//EXTRA TURN			
			
			//resolve extra turn
			GameManager.Instance.isTurnPaused = false;
			
		}
	}//End Turn Check


	public virtual void DisableAbilityPassive(){

	}//DisableAbilityPassive

	

}//Ability Class
