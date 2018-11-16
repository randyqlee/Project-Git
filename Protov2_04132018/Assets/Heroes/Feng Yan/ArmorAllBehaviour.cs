using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorAllBehaviour : SkillComponent {

//	public float value;
	public List<GameObject> targets;
	public int index;  // ID of Buff, to follow ID
	// Use this for initialization

	void Start () {
		
	}

	void OnDisable ()
	{
		if (targets != null)
		{
			for (int i=0; i<targets.Count; i++)
			{
				if(targets[i]!=null) {
			List<ModifyArmor> components = new List<ModifyArmor> ( targets[i].GetComponents<ModifyArmor>());
			foreach (ModifyArmor comp in components)
				if (comp.index == index)
				{
					Destroy (comp);
				}
			}
			}
		}

	}

	public override void UseSkill(GameObject source, GameObject target) {}

	public override void UseSkill ()
	{
		Debug.Log ("ArmorAll");
/*
		if (isActive)
		{
			targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Team1"));
			for (int i=0; i<targets.Count; i++)
			{
				if (targets[i] != gameObject)
				{

					targets[i].AddComponent<ArmorAllComponent>().New(targets[i].GetComponent<Hero>().attributes.baseArmor * (values[0]/100));
			List<ArmorAllComponent> components = new List<ArmorAllComponent> ( targets[i].GetComponents<ArmorAllComponent>());
			index = components.Count; 
				}
			}
			isActive = false;
			currCost = baseCost;
		}
*/

		if (IsSkillAllowed() && isActive)
		{
			targets = new List<GameObject>(GameObject.FindGameObjectsWithTag(gameObject.tag));
			for (int i=0; i<targets.Count; i++)
			{
				targets[i].AddComponent<ModifyArmor>().New(targets[i].GetComponent<Hero>().attributes.baseArmor * (values[0]/100));
				List<ModifyArmor> components = new List<ModifyArmor> ( targets[i].GetComponents<ModifyArmor>());
				index = components.Count; 
			}
			GameObject gameManager = GameObject.Find("Game Manager");
			gameManager.GetComponent<GlobalATB>().tempTimer = 0;
			gameObject.GetComponentInChildren<SkillPanel>().HidePanel();
			ResetSkill();
			e.popupMsg ("ArmorAll");
		}
		else e.popupMsg ("Skill not allowed");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
