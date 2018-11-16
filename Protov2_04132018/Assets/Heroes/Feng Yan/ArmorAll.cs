using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Skill/ArmorAll")]
public class ArmorAll : Skill {


	public float armorValue = 11;


	public override void Use(GameObject source, GameObject target){}

	public override void Use(){}

	void OnEnable ()
	{


	}

	void OnDisable ()
	{
	}


	public override void Use(GameObject source) {

		source.AddComponent<ArmorAllBehaviour>().New(cost, new float[]{armorValue});

	}




}
