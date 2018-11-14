using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectSoul : BuffComponent {

	void Awake () {
		this.tag = "Buff";
		this.buffName = "ProtectSoul";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Protect_Soul1");

		gameObject.GetComponent<Hero>().hasProtectSoul = true;
		
	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().hasProtectSoul = false;
		base.OnDestroy();
	}

}
