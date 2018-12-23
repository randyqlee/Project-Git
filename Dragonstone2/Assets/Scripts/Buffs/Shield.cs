using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Shield : Buff {

	//include listener for when hero takes damage

	[SerializeField]
	int remainingShield;


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Shield");

		//attach buf Icon to Hero UI
		this.buffIcon = buff.icon;

		//apply effect

		gameObject.GetComponent<HeroManager>().shield += buff.value;
		remainingShield = buff.value;
		//display shield icon
		gameObject.GetComponent<HeroManager>().transform.Find("HeroUI").gameObject.transform.Find("Shield").gameObject.SetActive(true);

		gameObject.GetComponent<HeroManager>().shieldText.text = gameObject.GetComponent<HeroManager>().shield.ToString();
		gameObject.GetComponent<HeroManager>().e_TakeDamage += CheckShieldValue;

		
		
	}

	// Use this for initialization
	
	void CheckShieldValue()
	{
		
		remainingShield = gameObject.GetComponent<HeroManager>().shield;		

		if (remainingShield <= 0){
			
			OnDestroy();		
			Destroy(this);
			
		}
		

	}

	protected override void OnDestroy()
	{
		//remove shield icon
		gameObject.GetComponent<HeroManager>().transform.Find("HeroUI").gameObject.transform.Find("Shield").gameObject.SetActive(false);
		//remove effect
		if (remainingShield > 0)
			gameObject.GetComponent<HeroManager>().shield -= remainingShield;
			gameObject.GetComponent<HeroManager>().UpdateUI();

		gameObject.GetComponent<HeroManager>().e_TakeDamage -= CheckShieldValue;

		

		//call parent OnDestroy
		base.OnDestroy();
	}

}
