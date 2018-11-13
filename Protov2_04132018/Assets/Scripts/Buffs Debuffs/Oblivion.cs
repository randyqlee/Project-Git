using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion : BuffComponent {

	void Awake () {
		this.tag = "Debuff";
		this.buffName = "Oblivion";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Oblivion");
		
		gameObject.GetComponent<Hero>().hasOblivion = true;
		
	}

	protected override void OnDestroy () {
		gameObject.GetComponent<Hero>().hasOblivion = false;
		base.OnDestroy();	
	}
}
