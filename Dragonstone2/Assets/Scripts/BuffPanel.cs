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

	public void AddIcon (string buffName, Sprite buffIcon)
	{
		buffImg = new GameObject();
		buffImg.name = buffName;
		buffImg.AddComponent<RectTransform>();

		buffImg.AddComponent<Image>().sprite = buffIcon;
		buffImg.transform.SetParent(panel, false);

		buffIcons.Add(buffImg);

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
