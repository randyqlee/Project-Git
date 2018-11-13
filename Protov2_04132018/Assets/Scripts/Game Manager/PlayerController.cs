using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public LayerMask selectionMask;
	int mask;

	void Awake ()
	{
		this.enabled = false;
		mask = LayerMask.GetMask("UI");
		selectionMask = new LayerMask();
		selectionMask.value = mask;
		
	}

	// Use this for initialization
	void Start () {
		

		StartCoroutine(WaitForMouseButtonDown());
		
	}

	IEnumerator WaitForMouseButtonDown()
	{
		while (true)
		{
			if (Input.GetMouseButton(0))
			{
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, selectionMask, 100);
				//switch for layermasks
/*
				switch (hit.collider.gameObject)
				{
					default:
					break;
				}
				if (hit.collider.gameObject.tag != "Team 1")
				{
					//
					yield break;
				}
				
*/
				if (hit)
					if (hit.collider.gameObject.tag == "Team1" || hit.collider.gameObject.tag == "Team2")
					{
						if (hit.collider.gameObject.GetComponent<Hero>().isActive)
						{
							ShowSkillPanel (hit.collider.gameObject);
							HideSelectionArrow ();
							
							ShowSelectionArrow (hit.collider.gameObject);
						}
					}

			}
			yield return null;

		}
	}

	public void HideSkillPanel ()
	{
		GameObject[] gameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject hero in gameObjects)
		{
			if (hero.tag == "Team1" || hero.tag == "Team2")
				if (hero.gameObject.GetComponentInChildren<SkillPanel>())
				{
					hero.gameObject.GetComponentInChildren<SkillPanel>().HidePanel();
				}
		}
	}

	public void ShowSkillPanel (GameObject hero)
	{
		HideSkillPanel();
		if(hero.gameObject.GetComponentInChildren<SkillPanel>())
		{
			hero.GetComponentInChildren<SkillPanel>().ShowPanel();
		}
		
	}

	void HideSelectionArrow ()
	{

	}

	void ShowSelectionArrow (GameObject hero)
	{
		hero.GetComponentInChildren<SelectArrow>().ShowArrow();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
