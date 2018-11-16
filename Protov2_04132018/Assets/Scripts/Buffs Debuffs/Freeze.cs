using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : BuffComponent {

	void Start () {
		this.tag = "Debuff";
		gameObject.GetComponent<Hero>().isFreeze = true;
	}

	void OnDisable ()
	{
		gameObject.GetComponent<Hero>().isFreeze = false;
	}
}
