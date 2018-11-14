using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour {
	public GameObject source;
	public GameObject target;

	// Use this for initialization
	void Start () {
		
	}

	public void UseSkill()
	{
		
	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.GetComponent<Hero>().IsActive)
		{ 
			if (Input.GetKeyDown ("y"))
			{
				gameObject.GetComponent<SkillSystem>().skillComponents [1].UseSkill (gameObject,target);
				Debug.Log (gameObject + " is using Skill: " + gameObject.GetComponent<SkillSystem>().skills[1].name.ToString());
				gameObject.GetComponent<Hero>().SetInactive();
			}
		}

		if (Input.GetKeyDown ("q"))
		{
			
			gameObject.GetComponent<BuffSystem>().AddBuffComponent<Brand>(2,gameObject);
		}





		/*

			if (Input.GetKeyDown ("q"))
			{
				if (!GetComponent<ModifyHP>())
				gameObject.AddComponent<ModifyHP>().value = 50;
			}

			if (Input.GetKeyDown ("w"))
			{
				Destroy (GetComponent<ModifyHP>());
			}

			if (Input.GetKeyDown ("e"))
			{
				gameObject.AddComponent<Defend>().New (source);
			}

			if (Input.GetKeyDown ("r"))
			{
				gameObject.GetComponent<SkillSystem>().skills[0].Use(gameObject);
			}

			if (Input.GetKeyDown ("t"))
			{
				//gameObject.GetComponent<SkillSystem>().skills[0].Use(gameObject);
				gameObject.GetComponent<ArmorAllBehaviour>().UseSkill();
			}
			if (Input.GetKeyDown ("y"))
			{
				gameObject.GetComponent<Hero>().skills[1].Use(gameObject,target);

			}
			if (Input.GetKeyDown ("u"))
			{
				List<BuffComponent> buffs = new List<BuffComponent>(gameObject.GetComponents<BuffComponent>());
				foreach (BuffComponent buff in buffs)
				{
					if (buff.tag == "Debuff")
					Destroy(buff);
				}
			}
			if (Input.GetKeyDown ("i"))
			{
				EventManager e;
				e=GetComponent<EventManager>();
				e.takeDamage(source, target);
			}

			if (Input.GetKeyDown ("o"))
			{
				GetComponent<Hero>().ModifyArmor(50);
			}

			if (Input.GetKeyDown ("p"))
			{
				EventManager e;
				e=GetComponent<EventManager>();
				e.resetATB();
			}

		*/

		
	}
}
