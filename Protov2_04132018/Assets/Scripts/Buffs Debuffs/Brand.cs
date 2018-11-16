using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brand : BuffComponent {

	// Use this for initialization
	void Awake () {
		this.tag = "Debuff";
		this.buffName = "Brand";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Brand");
		gameObject.GetComponent<Hero>().hasBrand = true;
		
	}

	protected override void OnDestroy () {
		gameObject.GetComponent<Hero>().hasBrand = false;
		base.OnDestroy();
	}
}
