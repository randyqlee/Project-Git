using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalResist : BuffComponent {

	public float value = 50;

	// Use this for initialization
	void Awake () {
		this.tag = "Buff";
		this.buffName = "CriticalResist";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Increase_Critical_Hit_Protection1");


		gameObject.GetComponent<Hero>().hasCriticalResist = true;
		
	}

	void OnDisable () {
		gameObject.GetComponent<Hero>().hasCriticalResist = false;
	}
}
