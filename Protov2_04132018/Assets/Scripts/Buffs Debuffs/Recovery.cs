using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : BuffComponent {

	EventManager e;

	public float value = 25;

	void Awake () {
		this.tag = "Buff";
		this.buffName = "Recovery";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Blessed1");

		e = GetComponent<EventManager>();
		e.e_ResetATB += Recover;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//LISTEN TO TURN EVENT

	//then call this
	void Recover () {
		gameObject.GetComponent<Hero>().currAttribs.baseHP += gameObject.GetComponent<Hero>().currAttribs.baseHP * (100+value)/100 ;
	}
}
