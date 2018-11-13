using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBuff: BuffComponent {

	void Awake () {
		this.tag = "Debuff";
		this.buffName = "AntiBuff";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/AntiBuff");

        gameObject.GetComponent<BuffSystem>().RemoveAllBuffs();
		gameObject.GetComponent<Hero>().isImmuneToPosEffects = true;
	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().isImmuneToPosEffects = false;
		base.OnDestroy();
	}
}
