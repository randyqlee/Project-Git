using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {

	public List<GameObject> team1;
	public List<GameObject> team2;
	public List<GameObject> all;


	void Awake () {

		team1 = new List<GameObject>(GameObject.FindGameObjectsWithTag("Team1"));
		team2 = new List<GameObject>(GameObject.FindGameObjectsWithTag("Team2"));


	}

	// Use this for initialization
	void Start () {
		all = new List<GameObject>();
		foreach (GameObject go in team1)
		all.Add(go);
		foreach (GameObject go in team2)
		all.Add(go);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
