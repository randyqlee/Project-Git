using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseImmunity : BuffComponent {

	void Awake () {
		this.tag = "Debuff";
		this.buffName = "ReverseImmunity";
		//this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Increase_Critical_Hit_Protection1");
		gameObject.GetComponent<Hero>().isImmuneToPosEffects = true;
	}

	void OnDisable ()
	{
		gameObject.GetComponent<Hero>().isImmuneToPosEffects = false;
	}
	
}
