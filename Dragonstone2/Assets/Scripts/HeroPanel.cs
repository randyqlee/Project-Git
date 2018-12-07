using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroPanel : MonoBehaviour {

	public HeroManager hero;

	public Image heroPortrait;
	public List<Button> skillsBtn;

	GameObject skillText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void CreateHeroPanel ()

	{
	
		heroPortrait.GetComponentInChildren<Image>().sprite = hero.image;
		for (int i = 0; i < hero.abilityAssets.Count; i++)
		{
			//map the icon and abilities to the button
			skillsBtn[i].image.sprite = hero.abilityAssets[i].icon;
			skillsBtn[i].gameObject.AddComponent(System.Type.GetType(hero.abilityAssets[i].abilityEffect));
			skillsBtn[i].gameObject.GetComponent<Ability>().abilityCooldown = hero.abilityAssets[i].abilityCoolDown;
			skillsBtn[i].gameObject.GetComponent<Ability>().remainingCooldown = hero.abilityAssets[i].abilityCoolDown;

			skillsBtn[i].gameObject.GetComponent<Ability>().abilityBuffs = hero.abilityAssets[i].abilityBuffs;

			skillsBtn[i].gameObject.GetComponent<Ability>().abilityDebuffs = hero.abilityAssets[i].abilityDebuffs;



			if (skillsBtn[i].GetComponent<TextMesh>() == null)
			{
			skillsBtn[i].gameObject.AddComponent<TextMesh>();
			}
			skillsBtn[i].GetComponent<TextMesh>().text = hero.abilityAssets[i].description;

			if (skillsBtn[i].gameObject.GetComponent<EventTrigger>() == null)
				{

				EventTrigger eventTrigger = skillsBtn[i].gameObject.AddComponent<EventTrigger>();

			EventTrigger.Entry pointerEntry = new EventTrigger.Entry( );
			pointerEntry.eventID = EventTriggerType.PointerEnter;
			pointerEntry.callback.AddListener( ( data ) => { OnPointerEnter( (PointerEventData)data ); } );
			eventTrigger.triggers.Add(pointerEntry);

			EventTrigger.Entry pointerExit = new EventTrigger.Entry( );
			pointerExit.eventID = EventTriggerType.PointerExit;
			pointerExit.callback.AddListener( ( data ) => { OnPointerExit( (PointerEventData)data ); } );
			eventTrigger.triggers.Add(pointerExit);
				}
			
		}

		
	}


	 public void OnPointerEnter( PointerEventData data )
      {
		  	if (skillText == null)
		skillText = GameObject.Find("Skill Text");

		  //skillText.SetActive(true);
		  skillText.GetComponent<Text>().text = data.pointerEnter.GetComponent<TextMesh>().text;
          Debug.Log (data.pointerEnter.GetComponent<TextMesh>().text);
      }
	
	 public void OnPointerExit( PointerEventData data )
	 {
		 //skillText.SetActive(false);
		 skillText.GetComponent<Text>().text = null;
	 }

	






}
