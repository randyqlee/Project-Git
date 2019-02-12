using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Debuff {

	// Use this for initialization
	void Awake () {
		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Poison");

		//attach buf Icon to Hero UI
		this.debuffIcon = debuff.icon;

		
	}
	//apply effect here
	public override void DecreaseDuration()
	{
		base.DecreaseDuration();
		
		if (GetComponentInParent<Player>().isActive && GetComponentInParent<HeroManager>().isSelected)
		{
			gameObject.GetComponent<HeroManager>().maxHealth -= debuff.value;
			GameManager.Instance.CheckHealth();
		}

		

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		//gameObject.GetComponent<HeroManager>().attack += buff.value;
	}

	public override void OnDestroy()
	{
		//remove effect

		//call parent OnDestroy
		base.OnDestroy();
	}

}
