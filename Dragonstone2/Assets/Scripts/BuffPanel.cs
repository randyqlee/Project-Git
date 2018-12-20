using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffPanel : MonoBehaviour {

	public List <GameObject> buffIcons;
	public RectTransform panel;

//	EventManager e;

	GameObject buffImg;


	// Use this for initialization
	void Start () {
		buffIcons = new List<GameObject>();

//		e = GetComponentInParent<EventManager>();
//		e.e_AddBuff += AddIcon;
//		e.e_RemoveBuff += RemoveIcon;
	}

	public void AddIcon (string buffName, Sprite buffIcon, int duration)
	{
		buffImg = new GameObject();
		buffImg.name = buffName;
		buffImg.AddComponent<RectTransform>();

		buffImg.AddComponent<Image>().sprite = buffIcon;


		buffImg.AddComponent<TextMesh>();
		TextMesh buffDurationText = buffImg.GetComponent<TextMesh>();
		buffDurationText.offsetZ = -0.15f;
		buffDurationText.characterSize = 0.09f;
		buffDurationText.fontSize = 18;
		buffDurationText.color = Color.black;
		buffDurationText.text = duration.ToString();

		buffImg.transform.SetParent(panel, false);

		buffIcons.Add(buffImg);





	}

	public void UpdateBuffIconCD(string buffName, int duration)
	{
		for(int i = buffIcons.Count - 1; i > -1; i--)
		{
			if (buffIcons[i].name == buffName)
			{
				buffIcons[i].GetComponent<TextMesh>().text = duration.ToString();
			}
		}


	}

	public void RemoveIcon (string buffName)
	{
		Debug.Log ("Removing Icon");

		for(int i = buffIcons.Count - 1; i > -1; i--)
		{
			if (buffIcons[i].name == buffName)
			{
				GameObject temp = buffIcons[i];
				buffIcons.RemoveAt(i);
				Destroy (temp);
			}
		}

	}
}
