using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stun : Debuff {

void Awake(){

		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Stun");

		//attach icon to Hero UI
		this.debuffIcon = debuff.icon;

		//enable heroPanel instance
		gameObject.GetComponent<HeroManager>().heroPanel.SetActive(true);

			List<Button> skillsButton = gameObject.GetComponentInChildren<HeroPanel>().skillsBtn;
			Debug.Log("Skills Button" +skillsButton);

			
			for(int i = 0; i <skillsButton.Count; i++){
				//disable the BoxCollider2D for the listener
				skillsButton[i].GetComponent<BoxCollider2D>().enabled = false;
				//disable the button's interactable for grey out effect
				skillsButton[i].interactable = false;
			}		

			//disable heroPanel again after setting colliders off
			gameObject.GetComponent<HeroManager>().heroPanel.SetActive(false);
	}

	//apply effect
	public override void DecreaseDuration(){

			
		base.DecreaseDuration();
	}//DecreaseDuration	


	// Update is called once per frame
	void Update () {
		
	}//Update

	protected override void OnDestroy(){

		//enable instance 
		gameObject.GetComponent<HeroManager>().heroPanel.SetActive(true);

			List<Button> skillsButton = gameObject.GetComponentInChildren<HeroPanel>().skillsBtn;
			Debug.Log("Skills Button" +skillsButton);

			
			for(int i = 0; i <skillsButton.Count; i++){
				//disable the BoxCollider2D for the listener
				skillsButton[i].GetComponent<BoxCollider2D>().enabled = true;
				//disable the button's interactable for grey out effect
				skillsButton[i].interactable = true;
			}		

			//disable instance after modifications
			gameObject.GetComponent<HeroManager>().heroPanel.SetActive(false);

			base.OnDestroy();

	}//OnDestroy
	
		
}//class

