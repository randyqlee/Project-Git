using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceCooldown : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Reduce Cooldown");
		
		this.buffIcon = buff.icon;

		gameObject.GetComponent<HeroManager>().hasReduceCooldown = true;

		GameManager.Instance.e_PlayerStartPhase += ReduceCD;


	}
	// Use this for initialization
	
	
	public override void OnDestroy()
	{

		
		GameManager.Instance.e_PlayerStartPhase -= ReduceCD;
		gameObject.GetComponent<HeroManager>().hasReduceCooldown = false;

		//call parent OnDestroy
		base.OnDestroy();
	}

	public void ReduceCD()
	{
		if(gameObject.GetComponent<HeroManager>().hasReduceCooldown && gameObject.GetComponentInParent<Player>().isActive) 
		{			
			
			//Reset Skills to Max Cooldown
			gameObject.GetComponent<HeroManager>().heroPanel.SetActive(true);
			Ability[] abilities = gameObject.GetComponent<HeroManager>().GetComponentsInChildren<Ability>();
			foreach (Ability ability in abilities){
			
			ability.remainingCooldown --;
			
			if(ability.remainingCooldown <0)
			ability.remainingCooldown = 0;

			gameObject.GetComponent<HeroManager>().UpdateUI();
			
			}
			gameObject.GetComponent<HeroManager>().heroPanel.SetActive(false);
		
		}		
		
	}//Recover
}
