using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public delegate void Event_TakeDamage(GameObject target, GameObject attacker);
	public event Event_TakeDamage e_TakeDamage = delegate {};

	public delegate void Event_PopupMsg(string message);
	public event Event_PopupMsg e_PopupMsg = delegate {};

	public delegate void Event_RemoveBuff(string buffName);
	public event Event_RemoveBuff e_RemoveBuff = delegate {};

	public delegate void Event_AddBuff(string buffName, Sprite buffIcon);
	public event Event_AddBuff e_AddBuff = delegate {};

	public delegate void Event_ResetATB();
	public event Event_ResetATB e_ResetATB = delegate {};

	public delegate void Event_CriticalHit();
	public event Event_ResetATB e_CriticalHit = delegate {};

	public delegate void Event_ResetStun();
	public event Event_ResetStun e_ResetStun = delegate {};

	public delegate void Event_HeroDie();
	public event Event_ResetATB e_HeroDie = delegate {};

	public delegate void Event_ModArmor();
	public event Event_ModArmor e_ModArmor = delegate {};

	public delegate void Event_ModDamage();
	public event Event_ModArmor e_ModDamage = delegate {};

	public delegate void Event_ModAccuracy();
	public event Event_ModArmor e_ModAccuracy = delegate {};

	public delegate void Event_ModHP();
	public event Event_ModArmor e_ModHP = delegate {};

	public delegate void Event_ModResistance();
	public event Event_ModArmor e_ModResistance = delegate {};

	public delegate void Event_ModSpeed();
	public event Event_ModArmor e_ModSpeed = delegate {};

	public delegate void Event_ModCR();
	public event Event_ModArmor e_ModCR = delegate {};
	//just to fix the issue: "Object not set to an instance" when using the delegate outside this code
	void Start () {

	}

	public void resetATB() {
		e_ResetATB();

	}

	public void criticalHit() {
		e_CriticalHit();

	}

	public void resetStun() {
		e_ResetStun();

	}

	public void heroDie() {
		e_HeroDie();

	}

	public void takeDamage(GameObject target, GameObject attacker) {
		e_TakeDamage(target,attacker);	

	}

	public void popupMsg (string message)
	{
		e_PopupMsg(message);
	}

	public void removeBuff (string buffName)
	{
		e_RemoveBuff(buffName);
	}

	public void addBuff (string buffName, Sprite buffIcon)
	{
		e_AddBuff(buffName, buffIcon);
	}

	public void modArmor()
	{
		e_ModArmor();
	}

	public void modDamage()
	{
		e_ModDamage();
	}

	public void modAccuracy()
	{
		e_ModAccuracy();
	}

	public void modHP()
	{
		e_ModHP();
	}

	public void modResistance()
	{
		e_ModResistance();
	}

	public void modSpeed()
	{
		e_ModSpeed();
	}

	public void modCR()
	{
		e_ModCR();
	}


}
