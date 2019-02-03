using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProtectSoul : Buff {

//increase attack damage by 50

	// Use this for initialization
	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Protect Soul");

		//attach buf Icon to Hero UI
		this.buffIcon = buff.icon;

		//apply effect
		gameObject.GetComponent<HeroManager>().hasProtectSoul = true;
		
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
		gameObject.GetComponent<HeroManager>().hasProtectSoul = false;

		//call parent OnDestroy
		base.OnDestroy();
	}

}
