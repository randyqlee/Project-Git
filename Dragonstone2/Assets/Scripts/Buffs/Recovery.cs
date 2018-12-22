using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Recovery");
		
		this.buffIcon = buff.icon;

		gameObject.GetComponent<HeroManager>().hasRecovery = true;

		GameManager.Instance.e_NextTurn += Recover;


	}
	// Use this for initialization
	
	
	protected override void OnDestroy()
	{

		
		GameManager.Instance.e_NextTurn -= Recover;
		gameObject.GetComponent<HeroManager>().hasRecovery = false;

		//call parent OnDestroy
		base.OnDestroy();
	}

	public void Recover()
	{
		if(gameObject.GetComponent<HeroManager>().hasRecovery && gameObject.GetComponentInParent<Player>().isActive) 
		{			
			
			//gameObject.GetComponent<HeroManager>().maxHealth += buff.value;
			GameManager.Instance.Heal(gameObject.GetComponent<HeroManager>(), buff.value);			
		
		}		
		
	}//Recover
}
