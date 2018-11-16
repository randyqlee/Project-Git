using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity : BuffComponent {


	void Awake () {
		this.tag = "Buff";
		this.buffName = "Immunity";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Immunity1");

		gameObject.GetComponent<Hero>().isImmuneToNegEffects = true;
	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().isImmuneToNegEffects = false;
		base.OnDestroy();
	}
	
}
