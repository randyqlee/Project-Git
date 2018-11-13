using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyArmor : AttribModifier {


	//initial addcomponent
	void Start ()
	{
		GetComponent<Hero>().currAttribs.baseArmor += value;
		Index();
	}

	//may be needed for enable/disable
	void OnEnable ()
	{
		GetComponent<Hero>().currAttribs.baseArmor += value;
	}

	void OnDisable ()
	{
		GetComponent<Hero>().currAttribs.baseArmor -= value;
	}

	public ModifyArmor New (float value)
	{
		this.value = value;
		return this;
	}

	public int Index ()
	{
		List<ModifyArmor> components = new List<ModifyArmor> ( gameObject.GetComponents<ModifyArmor>());
		this.index = components.Count;
		return this.index;
	}

}
