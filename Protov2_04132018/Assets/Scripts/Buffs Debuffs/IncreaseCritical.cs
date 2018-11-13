using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseCritical : BuffComponent {

	public float value = 25;
			//store the nominal "bonus" value for each buff
	public float bonus;

	// Use this for initialization
	void Awake () {
		this.tag = "Buff";
		this.buffName = "IncreaseCritical";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Increase Critical Rate");

		bonus = (value/100)*gameObject.GetComponent<Hero>().currAttribs.criticalRate;

		gameObject.GetComponent<Hero>().currAttribs.criticalRate += bonus;

	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().currAttribs.criticalRate -= bonus;
		base.OnDestroy();
	}
}
