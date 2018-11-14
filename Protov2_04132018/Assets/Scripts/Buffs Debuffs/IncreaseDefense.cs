using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDefense : BuffComponent {

	public float value = 50;


		//store the nominal "bonus" value for each buff
	public float bonus;

	void Awake () {
		this.tag = "Buff";
		this.buffName = "IncreaseDefense";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Increase_Defense");
		
		bonus = (value/100)*gameObject.GetComponent<Hero>().currAttribs.baseArmor;

		gameObject.GetComponent<Hero>().currAttribs.baseArmor += bonus;
	
	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().currAttribs.baseArmor -= bonus;
		base.OnDestroy();
	}

}
