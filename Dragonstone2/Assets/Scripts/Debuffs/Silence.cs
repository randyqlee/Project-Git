using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Silence : Debuff {

	Type skillTypeTemp;
	HeroManager hero;

	void Awake(){

		hero = gameObject.GetComponent<HeroManager>();
		
		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Silence");

		//attach icon to Hero UI
		this.debuffIcon = debuff.icon;

		hero.hasSilence = true;
		hero.heroPanel.SetActive(true);


			List<Button> skillsButton = gameObject.GetComponentInChildren<HeroPanel>().skillsBtn;
			//Debug.Log("Skills Button" +skillsButton);

			//start at i=1 to select 2nd and 3rd skill
			for(int i = 1; i <skillsButton.Count; i++){
						
				skillsButton[i].interactable = false;
				skillTypeTemp = skillsButton[i].GetComponent<Ability>().skillType;

				if(skillsButton[i].GetComponent<Ability>().skillType == Type.Active)
				skillsButton[i].GetComponent<Ability>().skillType = Type.Silenced;

			}		

			gameObject.GetComponent<HeroManager>().heroPanel.SetActive(false);
	}

	// //apply effect
	// public override void DecreaseDuration(){

			
	// 	base.DecreaseDuration();
	// }//DecreaseDuration	


	// Update is called once per frame
	void Update () {
		
	}//Update

	public override void OnDestroy(){

		gameObject.GetComponent<HeroManager>().heroPanel.SetActive(true);

			List<Button> skillsButton = gameObject.GetComponentInChildren<HeroPanel>().skillsBtn;
			//Debug.Log("Skills Button" +skillsButton);

			//start at i=1 to select 2nd and 3rd skill
			for(int i = 1; i <skillsButton.Count; i++){
				skillsButton[i].interactable = true;
				skillsButton[i].GetComponent<Ability>().skillType = skillTypeTemp;
			}		

			hero.heroPanel.SetActive(false);
			hero.hasSilence = false;

			base.OnDestroy();

	}//OnDestroy




}//Class - Silence
