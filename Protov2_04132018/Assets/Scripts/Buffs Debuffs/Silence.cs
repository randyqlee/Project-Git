using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silence : BuffComponent {

	void Awake () {
		this.tag = "Debuff";
		this.buffName = "Silence";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Silence");

		gameObject.GetComponent<Hero>().hasSilence = true;
	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().hasSilence = false;
		base.OnDestroy();
	}
}
