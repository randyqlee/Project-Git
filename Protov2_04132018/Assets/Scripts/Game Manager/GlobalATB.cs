using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalATB : MonoBehaviour {

//public static float globalATB = 1429;
	public float globalATB = 700;
	public float maxCharacterTurn = 10f; // maximum time allowed for character action
	public static bool tick = false;
	public static bool freezeTick = false;

	public delegate void Event_Tick();
	public static event Event_Tick e_Tick = delegate{};

	public delegate void Event_EndTurn();
	public static event Event_EndTurn e_EndTurn = delegate{};

	public List<Hero> heroes;
	public List<Hero> activeHeroes;

	[SerializeField] Image timerBar;
	public float tempTimer;
	float delta = 0.1f;

	void Awake ()
	{
		this.enabled = false;
	}


	// Use this for initialization
	void Start () {

		heroes = new List<Hero> (FindObjectsOfType<Hero>());
		activeHeroes = new List<Hero>();

		StartCoroutine (RunTick());
	
		
	}

	IEnumerator RunTick()
	{



			tick = false;
			yield return new WaitForSeconds(1);
			tick = true;
			//Debug.Log ("Tick");
			

			//trigger event for the heroes to increase a "tick"
			if (tick)
				e_Tick();

			//check if any hero reached full ATB
			if (!freezeTick)
				StartCoroutine (RunTick());

			else AllowHeroActions ();

	}

	bool IsHeroActive ()
	{
		foreach (Hero hero in heroes)
		{

			if (hero.GetComponentInParent<ATBTimer>().turn >= 1)
				return true;
		}

		return false;
	}

	void AllowHeroActions () {

		if (activeHeroes != null)
			activeHeroes.Clear();

		foreach (Hero hero in heroes)
		{
			if (!hero.GetComponentInParent<Hero>().isDead)
			{
				if (hero.GetComponentInParent<ATBTimer>()!=null) //just for debug
				if (hero.GetComponentInParent<ATBTimer>().turn >= 1)
					activeHeroes.Add (hero);
			}
		}

		BubbleSort (activeHeroes);

		StartCoroutine (StartAttackSequence (activeHeroes));


	}

//optimize this! There is a better way!
	void BubbleSort (List<Hero> activeHeroes)
	{
		if (activeHeroes.Count > 1)
		{
			for (int j = 0; j<activeHeroes.Count-1; j++)
			{
				for (int i=0; i<activeHeroes.Count-1; i++)
				{
					if (activeHeroes[i+1].GetComponentInParent<ATBTimer>().turn > activeHeroes[i].GetComponentInParent<ATBTimer>().turn)
					{
						Swap (activeHeroes, i);
					}
					else if (activeHeroes[i+1].GetComponentInParent<ATBTimer>().turn == activeHeroes[i].GetComponentInParent<ATBTimer>().turn)
					{
						if (activeHeroes[i+1].currAttribs.baseSpeed > activeHeroes[i].currAttribs.baseSpeed)
						{
							Swap (activeHeroes, i);
						}
						else if (activeHeroes[i+1].currAttribs.baseSpeed == activeHeroes[i].currAttribs.baseSpeed)
						{
							int rand = Random.Range (0, 2);
							if (rand == 1)
							{
								Swap (activeHeroes, i);
							}

						}
					}
				}
			}
		}
	}

	void Swap (List<Hero> activeHeroes, int i)
	{
		Hero c = activeHeroes[i];
		activeHeroes[i] = activeHeroes[i+1];
		activeHeroes[i+1] = c;

	}


	IEnumerator StartAttackSequence (List<Hero> activeHeroes)
	{
		foreach (Hero hero in activeHeroes)
		{
			
			if (!hero.GetComponentInParent<Hero>().isDead)
			{
				if (!hero.IsActive)
			//	hero.IsActive = true;
				hero.SetActive();
	/*			
				while (hero.IsActive)
				{ 	
					Debug.Log ("Begin Turn: " + gameObject);
					tempTimer = maxCharacterTurn;
					StartCoroutine (UpdateTimerBar(tempTimer));
					//hero.isActive = false; //temporary set isActive flag to FALSE already
					//hero.SetInactive();
					yield return new WaitForSeconds (maxCharacterTurn); //waiting time for hero action. Also within this period, the isActive flag can be set to TRUE, for another Turn.
					StopCoroutine (UpdateTimerBar(tempTimer));

					if (!hero.IsActive)
					{
						Debug.Log ("End of Turn: " + gameObject);
					}
				}
	*/
				Debug.Log ("Begin Turn: " + hero.name.ToString());


				
				hero.GetComponentInChildren<SelectArrow>().ShowArrow();
				GameObject gameManager = GameObject.Find("Game Manager");
				gameManager.GetComponent<PlayerController>().ShowSkillPanel(hero.gameObject);


/*
					//call methods to execute start of hero turn effects. ex. Healing
					if (hero.hasHealing)
					//heal all allies
					{
						Debug.Log ("Has Healing");
						List<GameObject> targets = new List<GameObject>(gameObject.GetComponent<GameManager>().all);
						
						foreach (GameObject target in targets)
						{
							if (target != null)
								if ( target.tag == hero.tag)
								{
									target.GetComponent<HealthSystem>().Heal(200f);
									Debug.Log ("Healing: " + target.name.ToString());
								}

						}
					}
*/

				tempTimer = maxCharacterTurn;
				while (tempTimer >0)
				{
					yield return new WaitForSeconds (delta);
					tempTimer -= delta;
					timerBar.fillAmount = tempTimer/maxCharacterTurn;
				}

				timerBar.fillAmount = 1;
				hero.SetInactive();

//just workaround this bug for now
				if (hero.GetComponentInChildren<SelectArrow>())
				hero.GetComponentInChildren<SelectArrow>().HideArrow();
				
				//broadcast that a hero ends a turn
				e_EndTurn();
			}

		}
		
		StartCoroutine (RunTick());
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable ()
	{
		StopAllCoroutines();
	}

}
