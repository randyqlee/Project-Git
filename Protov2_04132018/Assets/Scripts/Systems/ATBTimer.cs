using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ATBTimer : MonoBehaviour {

	public float baseSpeed;
	float rate;
	EventManager e;


	public float turn = 0;

	[SerializeField] Image ATBBar;

	public bool isPaused = false;

	public float tempAdder = 0;

	
	
	void Start () {
		//used to trigger Reset

		e = GetComponent<EventManager>();
		e.e_ResetATB += ResetTurn;
		GameObject gameManager = GameObject.Find("Game Manager");


		//computing ATB rate
		baseSpeed = GetComponent<Hero>().currAttribs.baseSpeed;		
		//rate = baseSpeed/GlobalATB.globalATB;

		rate = baseSpeed / gameManager.GetComponent<GlobalATB>().globalATB;

		//listening to global tick		
		GlobalATB.e_Tick += OnTick;		
	}
	// Update is called once per frame
	void Update () {

	}

	void OnTick (){

		if (GlobalATB.tick && !isPaused)
		{
			turn += rate;
			if (turn >= 1)
			{
				if (gameObject.GetComponent<Hero>().isStun)
				{
					//if hero is stunned, reset ATB but do not activate hero. in effect, the turn is skipped
					turn = 0;
					e.resetStun();
					e.popupMsg ("Stunned");
				}
				else
				{
					//if hero is not stunned when ATB reaches full, hero is activated
					GlobalATB.freezeTick = true;
					Debug.Log ("ATB freeze by: " + gameObject.name.ToString());

				}
				
			}
			UpdateATBBar (turn);
		}

	}

	public void ResetTurn()
	{
		turn = 0;

		//Quickly Zero ATB Bar when hero becomes active
		UpdateATBBar (turn);

		GlobalATB.freezeTick = false;
	}

	void UpdateATBBar (float turn)
	{
		ATBBar.fillAmount = turn;
	}

	public void modifyATB (float value)
	{
		turn = turn + value;
		UpdateATBBar (turn);

	}
}
