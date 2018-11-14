using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillComponent : MonoBehaviour {

	public float baseCost;
	public float currCost;
	public float[] values;
	public bool isActive = false;
	public SkillType skillType;
	public EventManager e;

	public bool IsSkillAllowed ()
	{
		bool isSkillAllowed = false;
		switch (this.skillType)
		{
			case SkillType.ACTIVE:
			if (!gameObject.GetComponent<Hero>().hasSilence)
				isSkillAllowed = true;
			break;

			case SkillType.PASSIVE:
			if (!gameObject.GetComponent<Hero>().hasOblivion)
				isSkillAllowed = true;
			break;
		}
		return isSkillAllowed;
	}

	public void ResetSkill()
	{
		this.currCost = this.baseCost;
		isActive = false;
	}
	

	public void New (int baseCost, float[] values)
	{
		this.baseCost = baseCost;
		this.values = values;
		currCost = baseCost;
	//	return this;
	}
	


	void Awake()
	{
		e = GetComponent<EventManager>();
		e.e_ResetATB += DecreaseCurrCost;
	}

	public void DecreaseCurrCost ()
	{
		if (this.currCost == 0)
			isActive = true;
		else
		{
			this.currCost--;
			if (this.currCost == 0)
				isActive = true;
		}	
	}

	public virtual void UseSkill () {}
	public virtual void UseSkill (GameObject source, GameObject target) {}

	public virtual void UseBattlecry () {}
	public virtual void UseDeathrattle () {}

	public virtual void UseLeaderSkill () {}

	public virtual void ShowTargets ()
	{


		GameObject gameManager = GameObject.Find("Game Manager");
		List<GameObject> goList = gameManager.GetComponent<GameManager>().all;
		
		if(gameObject.GetComponent<Provoke>())
		{
			gameObject.GetComponent<Provoke>().source.GetComponentInChildren<SelectArrow>().ShowArrow();
		}
		
		else 
		{
			List<GameObject> tauntHeroes = new List<GameObject>();
			List<GameObject> enemyHeroes = new List<GameObject>();
			foreach (GameObject hero in goList)
			{
				if (!hero.GetComponent<Hero>().isDead)

					if (hero.tag != tag )
					{

						//check if enemy has heroes with Taunt, then store them in a list
						
						if (hero.GetComponent<Hero>().hasTaunt)
						{
							tauntHeroes.Add(hero);
						}

						//add all enemy heroes in a list
						enemyHeroes.Add(hero);
						
					//	if ( hero.GetComponentInChildren<SelectArrow>())
					//	{
					//		hero.GetComponentInChildren<SelectArrow>().ShowArrow();
					//	}
					}
			}

			if (tauntHeroes.Count > 0 && gameObject.GetComponent<Hero>().heroClass != HeroClass.SUPPORT)
			{
								
				foreach (GameObject hero in tauntHeroes)
				{
					
					if ( hero.GetComponentInChildren<SelectArrow>())
					{
						hero.GetComponentInChildren<SelectArrow>().ShowArrow();
						hero.GetComponent<EventManager>().popupMsg ("Taunt!");
					}

				}
			}

			else
			{
				foreach (GameObject hero in enemyHeroes)
				{
					
					if ( hero.GetComponentInChildren<SelectArrow>())
					{
						
						hero.GetComponentInChildren<SelectArrow>().ShowArrow();
					}

				}
			}
		}

	}



	public virtual void HideTargets ()
	{
		GameObject gameManager = GameObject.Find("Game Manager");
		List<GameObject> goList = gameManager.GetComponent<GameManager>().all;
		foreach (GameObject hero in goList)
		{
			if ( hero.GetComponentInChildren<SelectArrow>())
			{
				hero.GetComponentInChildren<SelectArrow>().HideArrow();
			}
		}
	}

	public virtual IEnumerator WaitForMouseButtonDown()
	{
		while (true)
		{
			if (Input.GetMouseButton(0))
			{
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

				if (hit.collider != null)

					if(gameObject.GetComponent<Provoke>())
					{
						if (hit.collider.gameObject == gameObject.GetComponent<Provoke>().source)
						{
							hit.collider.gameObject.GetComponent<Hero>().isTargeted = true;
							UseSkill (gameObject,hit.collider.gameObject);
							HideTargets();
							new WaitForSeconds (2f);
				
							GameObject gameManager = GameObject.Find("Game Manager");
							gameManager.GetComponent<GlobalATB>().tempTimer = 0;
							hit.collider.gameObject.GetComponent<Hero>().isTargeted = false;
							yield break;
						}
					}

					else
					{
						if (hit.collider.gameObject.tag == "Team1" || hit.collider.gameObject.tag == "Team2")
						{
							if (hit.collider.gameObject.tag != tag)
							{
								hit.collider.gameObject.GetComponent<Hero>().isTargeted = true;
								UseSkill (gameObject,hit.collider.gameObject);
								HideTargets();
								new WaitForSeconds (2f);


								GameObject gameManager = GameObject.Find("Game Manager");
								gameManager.GetComponent<GlobalATB>().tempTimer = 0;
								hit.collider.gameObject.GetComponent<Hero>().isTargeted = false;
								gameObject.GetComponentInChildren<SkillPanel>().HidePanel();
								yield break;
							}
							else Debug.Log ("Target enemies only");
						}
					}

				
			}
			yield return null;
		}
		
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
