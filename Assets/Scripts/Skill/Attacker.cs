using System.Collections.Generic;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private Collider2D _collider;
    [SerializeField]private SkillDataSetting skillSetting;
    [SerializeField]private SkillData defaultSkill;
    [SerializeField] private GameTypeSettings typeSettings;
    private LinkedListNode<SkillData> _currentSkillNode;
    private LinkedList<SkillData> _skillList;
    private GameObject _canvas;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _canvas = GameObject.Find("Canvas");
        _skillList = new LinkedList<SkillData>(skillSetting.Skills);
        _currentSkillNode = _skillList.First;
        UpdateSkillDisplay(_currentSkillNode?.Value ?? defaultSkill);
    }

    private void UpdateSkillDisplay(SkillData data)
    {
        //Modify the display of Skill
        _canvas.GetComponentInChildren<SVGImage>().sprite = typeSettings.GetType(data.typeEnum).typeIcon;
        _canvas.GetComponentInChildren<TMP_Text>().text = $"{data.skillName}:{data.power}";
    }

    public void NextSkill(bool isPrevious = false)
    {
        if (isPrevious)
        {
            _currentSkillNode = _currentSkillNode?.Previous ?? _skillList.Last;
        }
        else _currentSkillNode = _currentSkillNode?.Next ?? _skillList.First;
        UpdateSkillDisplay(_currentSkillNode?.Value ?? defaultSkill);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_collider == col || col.CompareTag("Player")) return;
        if (col.TryGetComponent<Receiver>(out var opponent))
        {
            opponent.DealDamage(_currentSkillNode?.Value ?? defaultSkill);
            GetComponent<AudioSource>().Play();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextSkill(true);
            _canvas.GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            NextSkill();
            _canvas.GetComponent<AudioSource>().Play();
        }
    }
}
