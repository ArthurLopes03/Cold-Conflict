using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake() => skillTree = this;

    public int[] SkillLevels;
    public int[] SkillCaps;
    public string[] SkillNames;
    public string[] SkillDescriptions;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;

    public int SkillPoint;

    private void Start()
    {
        SkillPoint = 20;

        SkillLevels = new int[6];
        SkillCaps = new[] { 1, 5, 5, 2, 10, 10 };

        SkillNames = new[] { "Upgrade 1", "Upgrade 2", "Upgrade 3", "Upgrade 4", "Booster 5", "Booster 6", };
        SkillDescriptions = new[]
        {
         "Gives...",
         "Does...",
         "Grants",
         "Provides",
         "Will..",
         "This"
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);

        foreach (GameObject connector in ConnectorHolder.GetComponentInChildren<RectTransform>()) ConnectorList.Add(connector);

        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] { 1, 2, 3 };
        SkillList[2].ConnectedSkills = new[] { 4, 5 };

        UpdateAllSkillUI();

    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in SkillList) skill.UpdateUI();
    }
}
