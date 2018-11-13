using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour {

	public List<Skill> skills;
	public List<SkillComponent> skillComponents;

	public GameObject target;

	void Awake ()
	{
		skills = new List<Skill>();
		skillComponents = new List<SkillComponent>();
		foreach (Skill skill in GetComponent<Hero>().skills)
		{
			skills.Add(Instantiate( skill ));
		}

		foreach (Skill skill in skills)
		{
			skill.Use(gameObject);
		}

		foreach (SkillComponent skillComponent in GetComponentsInParent<SkillComponent>())
		{
			skillComponents.Add (skillComponent);
		}
	}

	public void ResetSkills()
	{
		foreach (SkillComponent skillComponent in GetComponentsInParent<SkillComponent>())
		{
			skillComponent.ResetSkill();
		}

	}

	public void ReplaceSkill(int index, Skill newSkill)
	{
/*
		foreach (SkillComponent skillComponent in GetComponentsInParent<SkillComponent>())
		{
			Destroy (skillComponent);
		}
*/

/*		foreach (SkillComponent skillComponent in skillComponents)
		{
			skillComponents.Remove (skillComponent);
		}
*/

		Destroy (skillComponents[index]);
		skills[index] = Instantiate (newSkill);

		newSkill.Use(gameObject);

		List<SkillComponent> newSkillComponents = new List<SkillComponent>();
		
		foreach (SkillComponent skillComponent in GetComponentsInParent<SkillComponent>())
		{
			newSkillComponents.Add (skillComponent);
		}

		skillComponents[index] = newSkillComponents[index+1];
		
	}



	// Use this for initialization
	void Start () {

	}
}
