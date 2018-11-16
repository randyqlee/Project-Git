using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour {

	public List<EventManager> globalEventManager;
	public List<GameObject> allHeroes;

	void Awake ()
	{
		globalEventManager = new List<EventManager>();
		allHeroes = GetComponent<GameManager>().all;
		foreach (GameObject go in allHeroes)
		{
			globalEventManager.Add(go.GetComponent<EventManager>());
		}

		foreach (EventManager e in globalEventManager)
		{
			//Debug.Log ("Subscribe to CheckRemainingHeroes");
			e.e_HeroDie += CheckRemainingHeroes;

		}

	}

	void CheckRemainingHeroes()
	{
		
		int team1 = 0;
		int team2 = 0;

		
		foreach (GameObject go in allHeroes)
		{
			if (!go.GetComponent<Hero>().isDead)
			{
				if (go.tag == "Team1")
				{
					team1++;
				}
				if (go.tag == "Team2")
				{
					team2++;
				}
			}
		}
		
		if (team1 == 0)
		{
			UnsubscribeHeroDie ();
			Debug.Log ("Team2 Wins");
			StartCoroutine ( gameObject.GetComponent<GameManager>().Winner ("Team2"));
		}
		if (team2 == 0)
		{
			UnsubscribeHeroDie ();
			Debug.Log ("Team1 Wins");
			StartCoroutine ( gameObject.GetComponent<GameManager>().Winner ("Team1"));
		}

	}

	void UnsubscribeHeroDie ()
	{
		foreach (EventManager e in globalEventManager)
		{
			e.e_HeroDie -= CheckRemainingHeroes;

		}
	}




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
