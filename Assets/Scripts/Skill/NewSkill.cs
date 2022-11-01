using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkill : MonoBehaviour
{
    public SkillData data;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameTypeSettings typeSettings;
    
    // Start is called before the first frame update
    void Start()
    {
        var typeIcon = typeSettings.GetType(data.typeEnum).typeIcon;
        spriteRenderer.sprite = typeIcon;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"{data.skillName} object Trigger Enter: {col.tag}");
        if (col.CompareTag("Attacker"))
        {
            col.GetComponent<Attacker>().AddSkill(data);
            Destroy(gameObject);
        }
        else if (col.CompareTag("Player"))
        {
            col.GetComponent<CrowController>().attackerComponent.AddSkill(data);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
