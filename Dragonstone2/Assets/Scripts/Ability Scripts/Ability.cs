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

	[HideInInspector]
	public List<HeroManager> extraTurnHeroes;
	

	public virtual void Awake ()
	{
		GameManager.Instance.e_PlayerStartPhase += GameManagerNextTurn;
		//GameManager.Instance.e_PlayerEndPhase += GameManagerNextTurn;		
		
	}

	public void OnDestroy(){
		GameManager.Instance.e_PlayerStartPhase -= GameManagerNextTurn;
	}

	public virtual void UseAbility (HeroManager attacker)
	{
		
		ResetCooldown();
		GameManager.Instance.ExtraTurnCheck(attacker); 
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
			GameManager.Instance.ExtraTurnCheck(attacker);

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
		GameManager.Instance.ExtraTurnCheck(attacker);

	}

	public virtual void UseAbilityPassive(){
		
		// Disable Access to skill UI
		GetComponent<Button>().interactable = false;
		GetComponentInChildren<Text>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
		
	}	

	public virtual void UseAbilityActive(){
		
				
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
		
		//TODO: check if hero is not in deadheroes	

		
		if (remainingCooldown > 0 && gameObject.GetComponentInParent<Player>().isActive && gameObject.GetComponentInParent<HeroManager>().isSelected)
		//if (remainingCooldown > 0 && gameObject.GetComponentInParent<Player>().isActive && GetComponentInParent<HeroManager>().gameObject != null)
		{
		
			remainingCooldown -= 1;
			if(remainingCooldown<=0){
				remainingCooldown = 0;

			}

			//update Button UI
			gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();
		}

		


	
	}//GameManager Next Turn

	


	public virtual void DisableAbilityPassive(){

	}//DisableAbilityPassive

	public virtual void DisableAbilityActive(){

	}//DisableAbilityPassive

	

}//Ability Class
