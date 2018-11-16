using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilitySystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool IsTrue (GameObject source, GameObject target, float definedProbability)
	{
		//float chance = definedProbability + source.GetComponent<Hero>().currAttribs.accuracy - target.GetComponent<Hero>().currAttribs.baseResistance;
		float chance = definedProbability;
		if ( (1-Random.value) < chance/100 )
		return true;

		else return false;
	} 

}
