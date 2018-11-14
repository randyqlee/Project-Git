using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTextController : MonoBehaviour {

	public GameObject text;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FloatingText (string message)
	{
		FloatingText popupText = Resources.Load<FloatingText>("Prefabs/BattleTextParent");
		FloatingText instance = Instantiate(popupText);
		instance.transform.SetParent (transform, false);
		instance.SetText(message);



	}

	public void SetText (string message)
	{
		text.GetComponent<Text>().text = message;
		StartCoroutine (PopupText());

	}

	void ShowText ()
	{
		text.gameObject.SetActive(true);
	}

	void HideText ()
	{
		text.gameObject.SetActive(false);
	}

	IEnumerator PopupText ()
	{
		ShowText();
		yield return new WaitForSeconds (1f);
		HideText();

		yield return null;

	}




}
