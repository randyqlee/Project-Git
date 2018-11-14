using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyAccuracy : AttribModifier {


	//initial addcomponent
	void Start ()
	{
		GetComponent<Hero>().currAttribs.accuracy += value;
		Index();
	}

	//may be needed for enable/disable
	void OnEnable ()
	{
		GetComponent<Hero>().currAttribs.accuracy += value;
	}

	void OnDisable ()
	{
		GetComponent<Hero>().currAttribs.accuracy -= value;
	}

	public ModifyAccuracy New (float value)
	{
		this.value = value;
		return this;
	}

	public int Index ()
	{
		List<ModifyAccuracy> components = new List<ModifyAccuracy> ( gameObject.GetComponents<ModifyAccuracy>());
		this.index = components.Count;
		return this.index;
	}

}
