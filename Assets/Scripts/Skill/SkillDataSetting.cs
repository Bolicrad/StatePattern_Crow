using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Skill Data Setting", order = 1)]
public class SkillDataSetting : ScriptableObject
{
    public List<SkillData> Skills;
}

[Serializable]
public struct SkillData
{
    public GameTypeEnum typeEnum;
    public string skillName;
    public int power;
}
