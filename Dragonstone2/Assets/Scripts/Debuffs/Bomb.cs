using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Debuff {

	// Use this for initialization
	void Awake () {
		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Bomb");
		//attach buf Icon to Hero UI
		this.debuffIcon = debuff.icon;		

	}//Awake

	//apply effect here
	public override void DecreaseDuration()
	{
		
		HeroManager hero = this.gameObject.GetComponent<HeroManager>();
		int bombDamage = debuff.value +	source.GetComponent<HeroManager>().attack;

		Debug.Log("Bomb Damage: " +bombDamage);
			
		
			if (GetComponentInParent<Player>().isActive)
			{
				this.duration--;

				//Deal damage and stun
				if (this.duration == 0)
				{					
					if(hero.hasImmunity || hero.hasPermanentImmunity)
						{
							Debug.Log("Hero has Immunity");
							GameManager.Instance.DealDamage(bombDamage, this.source.GetComponent<HeroManager>(), hero);

						}	
					else 
						{
							GameManager.Instance.AddDebuff("Stun",1, this.source.GetComponent<HeroManager>(), hero);
							GameManager.Instance.DealDamage(bombDamage, this.source.GetComponent<HeroManager>(), hero);
						}

					//GameManager.Instance.CheckHealth();							
						
					Destroy (this);
				}
					else
				{
					buffPanel.UpdateBuffIconCD(debuff.debuff.ToString(), this.duration);
				}
					
			}
		

	}//Decrease Duration	

	public override void OnDestroy()
	{
		//remove effect

		//call parent OnDestroy
		base.OnDestroy();
	}//OnDestroy

}//Class
