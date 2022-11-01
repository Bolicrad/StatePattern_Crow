using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private Collider2D _collider;
    [SerializeField]private List<SkillData> defaultSkills;
    [SerializeField] private GameTypeSettings typeSettings;
    [SerializeField] private GameObject skillPanel;
    private LinkedListNode<SkillData> _currentSkillNode;
    public LinkedList<SkillData> skillList;
    

    // Start is called before the first frame update
    

    public void Init()
    {
        skillPanel.SetActive(true);
        _collider = GetComponent<Collider2D>();
        skillList = new LinkedList<SkillData>(defaultSkills);
        SetCurrentSkill(skillList.First);
    }

    private void UpdateSkillDisplay(SkillData data)
    {
        //Modify the display of Skill
        skillPanel.GetComponentInChildren<SVGImage>().sprite = typeSettings.GetType(data.typeEnum).typeIcon;
        skillPanel.GetComponentInChildren<TMP_Text>().text = $"{data.skillName}:{data.power}";
    }

    public void NextSkill(bool isPrevious = false)
    {
        if (isPrevious)
        {
            SetCurrentSkill(_currentSkillNode?.Previous ?? skillList.Last);
        }
        else SetCurrentSkill(_currentSkillNode?.Next ?? skillList.First);
    }

    private void SetCurrentSkill(LinkedListNode<SkillData> node)
    {
        if (node == _currentSkillNode) return;
        if(_currentSkillNode!=null)GetComponent<AudioSource>().Play();
        _currentSkillNode = node;
        UpdateSkillDisplay(node.Value);
    }
    
    public void AddSkill(SkillData data)
    {
        if (skillList.Any(skillData => data.skillName == skillData.skillName))
        {
            return;
        }
        skillList.AddLast(data);
        SetCurrentSkill(skillList.Last);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_collider == col || col.CompareTag("Player")) return;
        if (col.TryGetComponent<Receiver>(out var opponent))
        {
            opponent.DealDamage(_currentSkillNode.Value);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextSkill(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            NextSkill();
        }
    }
}
