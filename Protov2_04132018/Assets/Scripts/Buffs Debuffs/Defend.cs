using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : BuffComponent {

	// Use this for initialization
	void Awake () {

		this.tag = "Buff";
		this.buffName = "Defend";

		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Defend1");

		//GetComponentInChildren<BuffPanel>().AddIcon(this.buffName, this.buffIcon);

		
	}


}
