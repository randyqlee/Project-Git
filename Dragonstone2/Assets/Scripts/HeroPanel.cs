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

	public void UpdateUI ()
	{
		if (skillText == null)
		skillText = GameObject.Find("Skill Text");

		heroPortrait.GetComponentInChildren<Image>().sprite = null;
		for (int i = 0; i < skillsBtn.Count; i++)
		{
			skillsBtn[i].image.sprite = null;
			skillsBtn[i].onClick.RemoveAllListeners();
			
		}

		heroPortrait.GetComponentInChildren<Image>().sprite = hero.image;
		for (int i = 0; i < hero.abilityAssets.Count; i++)
		{
			skillsBtn[i].image.sprite = hero.abilityAssets[i].icon;
			skillsBtn[i].onClick.AddListener (hero.abilities[i].UseAbility);

//for mouseover
			if (skillsBtn[i].GetComponent<TextMesh>() == null)
			{
			skillsBtn[i].gameObject.AddComponent<TextMesh>();
			}
			skillsBtn[i].GetComponent<TextMesh>().text = hero.abilityAssets[i].abilityName;
			

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
		  skillText.SetActive(true);
		  skillText.GetComponent<Text>().text = data.pointerEnter.GetComponent<TextMesh>().text;
          Debug.Log (data.pointerEnter.GetComponent<TextMesh>().text);
      }
	
	 public void OnPointerExit( PointerEventData data )
	 {
		 skillText.SetActive(false);
	 }





}
