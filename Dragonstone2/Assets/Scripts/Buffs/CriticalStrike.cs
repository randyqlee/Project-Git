using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalStrike : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Critical Strike");

		gameObject.GetComponent<HeroManager>().hasCritical = true;
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasCritical = false;
		//call parent OnDestroy
		base.OnDestroy();
	}
}
