using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unrecoverable : BuffComponent {

	void Awake () {
		this.tag = "Debuff";
		this.buffName = "Unrecoverable";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Unrecoverable");
		gameObject.GetComponent<Hero>().isUnrecoverable = true;
		
	}

	protected override void OnDestroy () {
		gameObject.GetComponent<Hero>().isUnrecoverable = false;
		base.OnDestroy();	
	}
}
