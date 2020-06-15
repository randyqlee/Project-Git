using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillEffect : MonoBehaviour
{
    //public Player owner;

    public int ID;
    public SkillAsset skillAsset;

    public string skillName;
    public string scriptName;
    public string description;
    public int cooldown;
    public Sprite thumbnail;
    
    public SkillType skillType;


}
