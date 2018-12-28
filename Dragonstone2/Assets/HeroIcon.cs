using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HeroIcon : MonoBehaviour, IPointerEnterHandler, ISelectHandler {

	public HeroAsset heroAsset;
	public Image heroImage;

	GameObject heroPreviewPanel;

	// Use this for initialization
	void Start () {

		heroImage.sprite = heroAsset.image;
		heroPreviewPanel = transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.Find("HeroPreviewPanel").gameObject;

	}

	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//do your stuff when highlighted
		UpdateHeroPreviewPanel();
	}

	public void UpdateHeroPreviewPanel()
	{
		heroPreviewPanel.transform.Find("ImagePanel").gameObject.GetComponentInChildren<Image>().sprite = heroAsset.image;
		heroPreviewPanel.transform.Find("ImagePanel").gameObject.GetComponentInChildren<Text>().text = heroAsset.heroName.ToString();

		Image[] abilityImage = heroPreviewPanel.transform.Find("AbilityPanel").GetComponentsInChildren<Image>();
		Text[] abilityText = heroPreviewPanel.transform.Find("AbilityPanel").GetComponentsInChildren<Text>();

		for (int i=0; i < heroAsset.abilityAsset2.Count; i++)
		{
			abilityImage[i].sprite = heroAsset.abilityAsset2[i].icon;
			abilityText[i].text = heroAsset.abilityAsset2[i].abilityName;

		}

	}


	public void OnSelect(BaseEventData eventData)
	{
		//do your stuff when selected
	}
}
