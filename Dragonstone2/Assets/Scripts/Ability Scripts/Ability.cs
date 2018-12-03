using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour {

	public int abilityCooldown;

	public int remainingCooldown;

	public bool canUseAbility;

	void Awake ()
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
