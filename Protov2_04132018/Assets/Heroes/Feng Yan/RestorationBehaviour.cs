using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorationBehaviour : SkillComponent {

	// Use this for initialization
	void Start () {



	}
	public override void UseSkill(GameObject source, GameObject target) {}

	public override void UseSkill ()
	{
		
		if (IsSkillAllowed() && isActive)
		{
			Debug.Log ("Restoration");

						//Remove all Debuff from self
			gameObject.GetComponent<BuffSystem>().RemoveAllDebuffs();

			//Add Counter Buff to Self for 2 Turn duration
			gameObject.GetComponent<BuffSystem>().AddBuffComponent<Counter>(2,gameObject);

			//Find all allies to target
			List<GameObject> targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Team1"));
			for (int i=0; i<targets.Count; i++)
			{
				if (targets[i] != gameObject)
				{
					//recover 15% HP of all allies
					targets[i].GetComponent<HealthSystem>().Heal(targets[i].GetComponent<Hero>().attributes.baseHP * values[0]/100);
				}
			}
			GameObject gameManager = GameObject.Find("Game Manager");
			gameManager.GetComponent<GlobalATB>().tempTimer = 0;
			gameObject.GetComponentInChildren<SkillPanel>().HidePanel();
			ResetSkill();
			e.popupMsg ("Restoration");
		}
		else e.popupMsg ("Skill not allowed");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
