using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

	public int abilityCooldown;

	public int remainingCooldown;

	public bool canUseAbility;

	void Start ()
	{
		GameManager.Instance.e_NextTurn += GameManagerNextTurn;
	}

	public virtual void UseAbility ()
	{

	}

	public virtual void UseAbility (HeroManager attacker, HeroManager defender)
	{


		Debug.Log ("Using ability: attacker - " + attacker.gameObject.name + " , defender - " + defender.gameObject.name);

	}

	public bool CanUseAbility()
	{	
		canUseAbility = false;
		if (remainingCooldown == 0)
		{
			canUseAbility = true;
			//ResetCooldown();
		}
		else Debug.Log ("Can't use ability"); 	
		return canUseAbility;



	}

	public void ResetCooldown()
	{
		remainingCooldown = abilityCooldown;
		canUseAbility = false;

	}

	public virtual void GameManagerNextTurn()
	{
		if (remainingCooldown > 0 && gameObject.GetComponentInParent<Player>().isActive)
		{
			remainingCooldown -= 1;
		}

	}
}
