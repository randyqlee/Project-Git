using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class BuffSystem : MonoBehaviour {

//	public List<Buff> buff;

	public void AddBuffComponent <T> (int duration, GameObject source) where T:BuffComponent
	{


		if (!gameObject.GetComponent<T>())
		{
			Type t = typeof (T);

			bool isBuff=false;
			bool isDebuff=false;

			string[] buffList = Enum.GetNames(typeof (BuffList));

			foreach (string buff in buffList)
			{
				if (t.ToString() == buff)
				{
					isBuff = true;
				}
			}

			string[] debuffList = Enum.GetNames(typeof (DebuffList));

			foreach (string debuff in debuffList)
			{
				if (t.ToString() == debuff)
				{
					isDebuff = true;
				}
			}

			if (isBuff)
			{
				if (!gameObject.GetComponent<Hero>().isImmuneToPosEffects)
				{
					gameObject.AddComponent<T> ().New(duration,source);
				}
				else Debug.Log ("Hero cannot be Buffed");
			}

			if (isDebuff)
			{
				if (!gameObject.GetComponent<Hero>().isImmuneToNegEffects)
				{
					if (!source.GetComponent<Hero>().hasGlancing)
					{
						gameObject.AddComponent<T> ().New(duration,source);
					}
				}
				else Debug.Log ("Hero cannot be Debuffed");
			}
		}

		else
		{
			Debug.Log ("Buff already exists");
			if (gameObject.GetComponent<T>().duration > duration)
				gameObject.GetComponent<T>().duration = duration;
		}
	}


	public void RemoveAllBuffs ()
	{
		List<BuffComponent> buffs = new List<BuffComponent>(gameObject.GetComponents<BuffComponent>());
		for(int i = buffs.Count - 1; i > -1; i--)
		{
			if (buffs[i].tag == "Buff")
			{
				var temp = buffs[i];
				buffs.RemoveAt(i);
				Destroy(temp);
			}
		}

	}

	public void RemoveAllDebuffs ()
	{
		List<BuffComponent> buffs = new List<BuffComponent>(gameObject.GetComponents<BuffComponent>());
		for(int i = buffs.Count - 1; i > -1; i--)
		{
			if (buffs[i].tag == "Debuff")
			{
				var temp = buffs[i];
				buffs.RemoveAt(i);
				Destroy(temp);
			}
		}
	}

	public void RemoveAll ()
	{
		List<BuffComponent> buffs = new List<BuffComponent>(gameObject.GetComponents<BuffComponent>());
		for(int i = buffs.Count - 1; i > -1; i--)
		{
				var temp = buffs[i];
				buffs.RemoveAt(i);
				Destroy(temp);
		}

	}


} 

/*
		if (!gameObject.GetComponent<T>())
		{
			GameObject targetObj = new GameObject();

			Type t = typeof (T);

			targetObj.AddComponent<T>();

			String tag = targetObj.GetComponent<T>().tag;

			if (tag == "Buff")
			{
				if (!gameObject.GetComponent<Hero>().isImmuneToPosEffects)
				{
					gameObject.AddComponent<T> ().New(duration);
				}
				else Debug.Log ("Hero cannot be Buffed");
			}

			if (tag == "Debuff")
			{
				if (!gameObject.GetComponent<Hero>().isImmuneToNegEffects)
				{
					gameObject.AddComponent<T> ().New(duration);
				}
				else Debug.Log ("Hero cannot be Debuffed");
			}
			Destroy (targetObj);
	
		}

		else
		{
			Debug.Log ("Buff already exists");
			if (gameObject.GetComponent<T>().duration > duration)
				gameObject.GetComponent<T>().duration = duration;
		}
	}
} 

*/

/*

//can we change this to accept generic components
	public void AddBuff (Buffs buffs, int duration)
	{

//Add switch statement for each Buff
		switch (buffs)
		{
			case Buffs.decDEF:
			{
				if (!gameObject.GetComponent<DecreaseDefense>())
				gameObject.AddComponent<DecreaseDefense>().New(duration);

				else
				{
					Debug.Log ("Buff already exists");
					gameObject.GetComponent<DecreaseDefense>().duration = duration;
				}

			}
			break;

			//add other Buffs here
		}

	}






	int SearchForBuff (Buffs buffs)
	{
		for (int i = 0; i < buff.Count; i ++)
		{
			if (buffs.Equals (buff[i].buff))
			{
				return i;
			}
		}
		return 100;	//100 is just a temporary number to say that Buff doesn't exist	
	}

	public void RemoveBuff (Buffs buffs)
	{
		int i = SearchForBuff(buffs);

		if (i != 100)
		{
			buff.RemoveAt(i);
		}
	}

	public void decreaseBuffTime (Buffs buffs)
	{
		if ( SearchForBuff (buffs) != 100)
		{
			buff[SearchForBuff(buffs)].duration -= 1;
			if (buff[SearchForBuff(buffs)].duration == 0)
				RemoveBuff (buff[SearchForBuff(buffs)].buff);
		}

	}

	public void decreaseAllBuffsTime ()
	{
		for (int i = 0; i < buff.Count; i ++)
		{
			decreaseBuffTime (buff[i].buff);
		}

	}




}
*/