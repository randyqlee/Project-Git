using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provoke : BuffComponent {

//Check for Provoke from targetting system of each skill

	// Use this for initialization
	void Awake () {
		this.tag = "Debuff";
		this.buffName = "Provoke";
		//this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Increase_Critical_Hit_Protection1");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



}
