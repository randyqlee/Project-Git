using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endure : BuffComponent {


	void Awake () {
		this.tag = "Buff";
		this.buffName = "Endure";		
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Endure1");
		
		gameObject.GetComponent<Hero>().isImmortal = true;

	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().isImmortal = false;
		base.OnDestroy();
	}
}
