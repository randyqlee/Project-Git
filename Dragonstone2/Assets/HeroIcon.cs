using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HeroIcon : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IEndDragHandler {

	public HeroAsset heroAsset;
	public Image heroImage;

	GameObject heroPreviewPanel;

	GameObject myCollection;

	public Image heroIcon;

	bool dragging = false;

	Vector2 origPosition;

	// Use this for initialization
	void Start () {

		heroImage.sprite = heroAsset.image;
		heroPreviewPanel = transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.Find("HeroPreviewPanel").gameObject;

		myCollection = transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.gameObject;
		
		UpdateHeroPreviewPanel();


		//Improve this
		//heroIcon.sprite = heroAsset.image;
		//heroIcon.gameObject.transform.SetParent(heroPreviewPanel.transform,false);


        //Fetch the Event Trigger component from your GameObject
        EventTrigger trigger = GetComponent<EventTrigger>();
        //Create a new entry for the Event Trigger
        EventTrigger.Entry entry = new EventTrigger.Entry();
        //Add a Drag type event to the Event Trigger
        entry.eventID = EventTriggerType.Drag;
        //call the OnDragDelegate function when the Event System detects dragging
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        //Add the trigger entry
        trigger.triggers.Add(entry);

	}

	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//do your stuff when highlighted
		if (!dragging)
		{
		UpdateHeroPreviewPanel();
		origPosition = transform.position;
		}
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


    public void OnDragDelegate(PointerEventData data)
    {
        //Create a ray going from the camera through the mouse position
        //Ray ray = Camera.main.ScreenPointToRay(data.position);
        //Calculate the distance between the Camera and the GameObject, and go this distance along the ray
        //Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));

		dragging = true;

		//heroIcon.gameObject.SetActive(true);

		Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		
        //Move the GameObject when you drag it
        //transform.position = rayPoint;
		transform.position = cursorPosition;

		GetComponent<BoxCollider2D>().enabled = false;

		
		//heroIcon.gameObject.transform.position = cursorPosition;
    }

	public void OnEndDrag(PointerEventData eventData)
    {
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.transform.tag == "DeckSlots")
        {
			
			transform.GetComponent<RectTransform>().anchoredPosition = transform.parent.position;
			//transform.GetComponent<RectTransform>().
			transform.GetComponent<RectTransform>().localPosition = Vector3.zero;
			transform.GetComponent<RectTransform>().sizeDelta = Vector3.zero;
		    transform.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
    		transform.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
    		transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
			
			transform.SetParent(hit.transform,false);

			myCollection.GetComponent<MyCollection>().playerDeckHeroAssets.Add(heroAsset);

			
			//hit.transform.GetComponent<Image>().sprite = heroIcon.sprite;
			//heroIcon.gameObject.SetActive(false);

		}

		else if ((hit.collider != null && hit.transform.tag == "AllHeroesScrollPanel"))
		{
			//return hero from deck

		}

        else 
		{
			dragging = false;
			GetComponent<BoxCollider2D>().enabled = true;
			transform.position = origPosition;

		}
    }

	public void OnSelect(BaseEventData eventData)
	{
		//do your stuff when selected
	}
}
