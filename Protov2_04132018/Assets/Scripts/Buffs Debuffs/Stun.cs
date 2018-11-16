using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : BuffComponent {

	void Awake () {
		this.tag = "Debuff";
		this.buffName = "Stun";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Stun");		
		gameObject.GetComponent<Hero>().isStun = true;
	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().isStun = false;
		base.OnDestroy();
	}
}
