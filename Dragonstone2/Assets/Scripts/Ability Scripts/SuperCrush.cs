using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCrush : Ability {


	public override void UseAbility ()
	{
		Debug.Log ("Using  SuperCrush");

	}

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{

			Debug.Log ("Using  SuperCrush");

			GameManager.Instance.Attack (attacker, defender);
			//TO Be FIXED
			/*

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

			*/
		

	}
}
