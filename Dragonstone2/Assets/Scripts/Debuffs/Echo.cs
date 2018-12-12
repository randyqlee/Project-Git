using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : Debuff {

	// Use this for initialization
	void Awake () {

		//get debuff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Crippled Strike");

		//attach debuff Icon to Hero UI
		this.debuffIcon = debuff.icon;

		//apply effect
		gameObject.GetComponent<HeroManager>().hasEcho = true;
		
	}
	

	protected override void OnDestroy(){
		gameObject.GetComponent<HeroManager>().hasEcho = false;

		base.OnDestroy();
	}



	// Update is called once per frame
	void Update () {
		
	}
}
