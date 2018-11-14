using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revenge : BuffComponent {

	EventManager e;
	public float value = 50;

	// Use this for initialization
	void Awake () {
		this.tag = "Buff";
		this.buffName = "Revenge";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Ally_Counterattack1");

		e = source.GetComponent<EventManager>();
		e.e_TakeDamage += RevengeAttack;
/*
		List<EventManager> globalEventManager = GameObject.Find("Game Manager").GetComponent<GlobalEventManager>().globalEventManager;

		foreach (EventManager globalEvent in globalEventManager)
		{
			//select events of same team
			if (globalEvent.gameObject.tag == gameObject.tag)
			{	
				//exclude self
				if (globalEvent.gameObject != gameObject)
					//subscribe
					globalEvent.e_TakeDamage += RevengeAttack;
			}
		}
*/
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	//USE DELEGATE TO LISTEN TO OTHER ALLIES BEING ATTACKED

	//PUT HERE THE REVENGE SCRIPT TO ATTACK (WITH PROBABILITY) THE ENEMY

	void RevengeAttack (GameObject target, GameObject source)
	{
		if (target.GetComponent<Hero>().isTargeted)
		{
			if ((1-Random.value) < value/100)
			{
				gameObject.GetComponent<DamageSystem>().BasicAttack(gameObject, source, 1, 0);
				e.popupMsg("Revenge!");
			}
		}
	}

}
