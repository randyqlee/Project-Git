using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glancing: BuffComponent {

	void Awake () {
		this.tag = "Debuff";
		this.buffName = "Glancing";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Glancing");

		gameObject.GetComponent<Hero>().hasGlancing = true;
	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().hasGlancing = false;
		base.OnDestroy();
	}
}
