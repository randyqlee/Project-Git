using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour {

	public int abilityCooldown;

	public int remainingCooldown;

	public bool canUseAbility;

	public List<AbilityBuffs> abilityBuffs;
	public List<AbilityDebuffs> abilityDebuffs;

	void Awake ()
	{
		GameManager.Instance.e_NextTurn += GameManagerNextTurn;
	}

	public virtual void UseAbility ()
	{

	}

	public virtual void UseAbility (HeroManager attacker, HeroManager defender)
	{

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
		gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();

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

	}
}
