using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyHP : AttribModifier {

	//initial addcomponent
	void Start ()
	{

		GetComponent<Hero>().currAttribs.baseHP += value;
		Index();
	}

	//may be needed for enable/disable
	void OnEnable ()
	{
		
		GetComponent<Hero>().currAttribs.baseHP += value;

	}

	void OnDisable ()
	{

	}

	public ModifyHP New (float value)
	{
		this.value = value;
		return this;
	}

	public int Index ()
	{
		List<ModifyHP> components = new List<ModifyHP> ( gameObject.GetComponents<ModifyHP>());
		this.index = components.Count;
		return this.index;
	}

}
