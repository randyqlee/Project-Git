using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachCrush : Ability {

	public override void UseAbility ()
	{
		Debug.Log ("Using  MachCrush");

	}

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		Debug.Log ("Using  MachCrush");

		foreach (Player player in GameManager.Instance.Players )
		{

			if (player.tag != GetComponentInParent<HeroManager>().tag)
			{
				foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
				{
					GameManager.Instance.Attack (attacker, hero);
				}
			}
		}		
	}
}

