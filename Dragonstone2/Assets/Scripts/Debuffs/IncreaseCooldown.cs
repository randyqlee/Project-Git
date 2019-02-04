using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseCooldown : Debuff {


	void Awake () {
		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Increase Cooldown");
		
		this.debuffIcon = debuff.icon;

		gameObject.GetComponent<HeroManager>().hasReduceCooldown = true;

		GameManager.Instance.e_PlayerStartPhase += IncreaseCD;


	}
	// Use this for initialization
	
	
	public override void OnDestroy()
	{

		
		GameManager.Instance.e_PlayerStartPhase -= IncreaseCD;
		gameObject.GetComponent<HeroManager>().hasReduceCooldown = false;

		//call parent OnDestroy
		base.OnDestroy();
	}

	public void IncreaseCD()
	{
		if(gameObject.GetComponent<HeroManager>().hasReduceCooldown && gameObject.GetComponentInParent<Player>().isActive) 
		{			
			
			//Reset Skills to Max Cooldown
			gameObject.GetComponent<HeroManager>().heroPanel.SetActive(true);
			Ability[] abilities = gameObject.GetComponent<HeroManager>().GetComponentsInChildren<Ability>();
			foreach (Ability ability in abilities){
			
			ability.remainingCooldown += this.debuff.value;
			
			if(ability.remainingCooldown <0)
			ability.remainingCooldown = 0;

			ability.GetComponentInChildren<Text>().text = ability.remainingCooldown.ToString();
			
			//gameObject.GetComponent<HeroManager>().UpdateUI();
			
			}
			gameObject.GetComponent<HeroManager>().heroPanel.SetActive(false);
		
		}		
		
	}//Recover
}
