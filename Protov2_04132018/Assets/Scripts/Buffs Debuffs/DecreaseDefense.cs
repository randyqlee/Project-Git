using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseDefense : BuffComponent{

	public float value = -50;


		//store the nominal "bonus" value for each buff
	public float bonus;


	void Awake () {
		this.tag = "Debuff";
		this.buffName = "DecreaseDefense";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Decrease_Defense");

		bonus = (value/100)*gameObject.GetComponent<Hero>().currAttribs.baseArmor;

		gameObject.GetComponent<Hero>().currAttribs.baseArmor += bonus;
					
	} 

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().currAttribs.baseArmor -= bonus;
		base.OnDestroy();
	}

}
