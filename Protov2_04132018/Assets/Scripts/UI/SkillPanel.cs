using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillPanel : MonoBehaviour {

	List <GameObject> buttons;
	public RectTransform panel;

	//public delegate void TestDelegate(string test); // This defines what type of method you're going to call.
    //public TestDelegate m_methodToCall; // This is the variable holding the method you're going to call.

	public delegate void ButtonSkill();
	public ButtonSkill buttonSkill;

	public EventManager e;

	GameObject skillText;

	void Awake()
	{
		e = GetComponentInParent<EventManager>();
		e.e_ResetATB += UpdateButtonFill;
	}

	// Use this for initialization
	void Start () {

		CreateButtons();
		skillText = GameObject.Find("Skill Text");
		//skillText.SetActive(false);

			
	}


	void CreateButtons()
	{
		buttons = new List<GameObject>();
		for (int i=0; i<3; i++)
		{
			Sprite sprite = GetComponentInParent<Hero>().skills[i].objectInfo.icon;
			//buttonSkill = GetComponentInParent<SkillSystem>().skillComponents[i].UseSkill;
			//buttonSkill = GetComponentInParent<SkillSystem>().Use;

			GameObject button = new GameObject();
			button.transform.SetParent(panel, false);
			button.AddComponent<RectTransform>();
			button.AddComponent<Button>();

			button.AddComponent<Image>().sprite = sprite;
			button.GetComponent<Button>().targetGraphic = button.GetComponent<Image>();


			//button.GetComponent<Button>().onClick.AddListener(() => buttonSkill());
			button.GetComponent<Button>().onClick.AddListener(GetComponentInParent<SkillSystem>().skillComponents[i].UseSkill);
			

			button.AddComponent<TextMesh>();
			button.GetComponent<TextMesh>().text = GetComponentInParent<Hero>().skills[i].objectInfo.description;

			EventTrigger eventTrigger = button.AddComponent<EventTrigger>();

          EventTrigger.Entry pointerEntry = new EventTrigger.Entry( );
          pointerEntry.eventID = EventTriggerType.PointerEnter;
          pointerEntry.callback.AddListener( ( data ) => { OnPointerEnter( (PointerEventData)data ); } );
          eventTrigger.triggers.Add(pointerEntry);

		EventTrigger.Entry pointerExit = new EventTrigger.Entry( );
		  pointerExit.eventID = EventTriggerType.PointerExit;
		  pointerExit.callback.AddListener( ( data ) => { OnPointerExit( (PointerEventData)data ); } );
          eventTrigger.triggers.Add(pointerExit);


		  GameObject buttonTimer = new GameObject();
		  
		  buttonTimer.transform.SetParent(button.transform,false);
		  buttonTimer.AddComponent<RectTransform>();
		  
		  buttonTimer.GetComponent<RectTransform>().anchorMin = new Vector2 (0,0);
		  buttonTimer.GetComponent<RectTransform>().anchorMax = new Vector2 (1,1);
		  buttonTimer.GetComponent<RectTransform>().sizeDelta = new Vector2 (0,0);

		  buttonTimer.AddComponent<Image>();
		  buttonTimer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Life Black");
		  buttonTimer.GetComponent<Image>().raycastTarget = false;
		  buttonTimer.GetComponent<Image>().type = Image.Type.Filled;
		  buttonTimer.GetComponent<Image>().fillAmount = 1;
			buttonTimer.GetComponent<Image>().color = new Color(1,1,1,0.8f);
		  buttons.Add(buttonTimer);

		  




			//pass the listener here
			//AddButton ( buttons[i],sprite,buttonSkill);
		}
		panel.gameObject.SetActive(false);
		UpdateButtonFill();


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

	  public void UpdateButtonFill()
	  {

		

		SkillComponent[] skills = new SkillComponent[3];
		if (GetComponentsInParent<SkillComponent>() != null && buttons != null )
		{
			skills = GetComponentsInParent<SkillComponent>();
			for (int i=0; i<3; i++)
			{
				if (skills[i].baseCost != 0)
				{
					float fill = skills[i].currCost / skills[i].baseCost;
					
				buttons[i].GetComponent<Image>().fillAmount = fill;
				
				}
			}
		}


	  }


	public void ShowPanel ()
	{
		panel.gameObject.SetActive(true);

	}

	public void HidePanel ()
	{
		panel.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("w"))
		{
			if (panel.gameObject.activeSelf)
			panel.gameObject.SetActive(false);

			else
			panel.gameObject.SetActive(true);

		
		}
		
	}
/*
	public void CreateButton(Transform panel ,Vector3 position, Vector2 size, UnityEngine.Events.UnityAction method)
	{
		GameObject button = new GameObject();
		button.transform.parent = panel;
		button.AddComponent<RectTransform>();
		button.AddComponent<Button>();
		button.transform.position = position;
		button.GetComponent<RectTransform>().SetSize(size);
		button.GetComponent<Button>().onClick.AddListener(method);
	}
*/


}
