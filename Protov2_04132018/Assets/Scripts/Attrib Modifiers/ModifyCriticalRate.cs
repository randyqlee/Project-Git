using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyCriticalRate : AttribModifier {


	//initial addcomponent
	void Start ()
	{
		GetComponent<Hero>().currAttribs.criticalRate += value;
		Index(); 
	}

	//may be needed for enable/disable
	void OnEnable ()
	{
		GetComponent<Hero>().currAttribs.criticalRate += value;
	}

	void OnDisable ()
	{
		GetComponent<Hero>().currAttribs.criticalRate -= value;
	}

	public ModifyCriticalRate New (float value)
	{
		this.value = value;
		return this;
	}

	public int Index ()
	{
		List<ModifyCriticalRate> components = new List<ModifyCriticalRate> ( gameObject.GetComponents<ModifyCriticalRate>());
		this.index = components.Count;
		return this.index;
	}

}
