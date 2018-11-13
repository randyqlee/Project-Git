using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : BuffComponent {

	public float value = 125;
	public int stunTurn = 1;
	float damage;

	
	// Use this for initialization
	void Awake () {
		this.tag = "Debuff";
		this.buffName = "Bomb";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Bomb");

		damage = source.GetComponent<Hero>().currAttribs.baseDamage * (value/100);
	}

	protected override void OnDestroy ()
	{
		//damage target when the bomb debuff is destroyed
		//how about when the Bomb was diffused????
		gameObject.GetComponent<HealthSystem>().TakeDamage(damage);

		//Stun for 1 turn
		gameObject.GetComponent<BuffSystem>().AddBuffComponent<Stun>(stunTurn,source);
		
		//remove Bomb debuff
		base.OnDestroy();
	}



}
