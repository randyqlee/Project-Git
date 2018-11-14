using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour {

	public List<Ability> abilities;

	// Use this for initialization
	void Start () {
		abilities = new List<Ability> (GetComponent<Character>().hero.abilities);
		for (int i = 0; i < abilities.Count; i++)
		{
			abilities[i].setParent(gameObject);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
