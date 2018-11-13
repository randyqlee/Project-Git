using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : BuffComponent {

	void Awake () {
		this.tag = "Buff";
		this.buffName = "Invincibility";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Invincibility1");
		gameObject.GetComponent<Hero>().isInvincible = true;
		
	}

	protected override void OnDestroy () {
		gameObject.GetComponent<Hero>().isInvincible = false;
		base.OnDestroy();
	}
}
