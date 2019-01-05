using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stun : Debuff {

	Type skillTypeTemp;
	Color origColor;
	HeroManager hero;
void Awake(){

		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Stun");

		//attach icon to Hero UI
		this.debuffIcon = debuff.icon;

		hero = gameObject.GetComponent<HeroManager>();

		//enable heroPanel instance
		hero.heroPanel.SetActive(true);

			List<Button> skillsButton = gameObject.GetComponentInChildren<HeroPanel>().skillsBtn;		

			
			for(int i = 0; i <skillsButton.Count; i++){

				skillsButton[i].interactable = false;
				skillTypeTemp = skillsButton[i].GetComponent<Ability>().skillType;
				skillsButton[i].GetComponent<Ability>().skillType = Type.Stunned;
			}		

			//disable heroPanel again after setting colliders off
			gameObject.GetComponent<HeroManager>().heroPanel.SetActive(false);

			//change color			
			origColor = hero.transform.Find("HeroUI").gameObject.transform.Find("Image").GetComponent<Image>().color;
			hero.transform.Find("HeroUI").gameObject.transform.Find("Image").GetComponent<Image>().color = Color.gray;
	}

	// //apply effect
	// public override void DecreaseDuration(){

		
	// 		base.DecreaseDuration();
					
		
	// }//DecreaseDuration	


	// Update is called once per frame
	void Update () {
		
	}//Update

	public override void OnDestroy(){

		//enable instance 
		gameObject.GetComponent<HeroManager>().heroPanel.SetActive(true);

			List<Button> skillsButton = gameObject.GetComponentInChildren<HeroPanel>().skillsBtn;
			//Debug.Log("Skills Button" +skillsButton);

			
			for(int i = 0; i <skillsButton.Count; i++){
				skillsButton[i].interactable = true;
				skillsButton[i].GetComponent<Ability>().skillType = skillTypeTemp;
			}		

			//disable instance after modifications
			gameObject.GetComponent<HeroManager>().heroPanel.SetActive(false);

			//restorecolor
			hero.transform.Find("HeroUI").gameObject.transform.Find("Image").GetComponent<Image>().color = origColor;
			

			base.OnDestroy();

	}//OnDestroy
	
		
}//class

