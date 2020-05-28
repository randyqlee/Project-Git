using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Skill", menuName = "SO/Skills")]
public class SkillAsset : ScriptableObject
{
    public string skillName;
    public string scriptName;
    public string description;
    public int cooldown;
    public Sprite thumbnail;
    public SkillType skillType;
}
