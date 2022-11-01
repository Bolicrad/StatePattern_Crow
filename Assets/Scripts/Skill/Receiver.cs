using System;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int maxHp;
    [SerializeField] private GameTypeEnum typeId;
    [SerializeField] private GameTypeSettings typeSettings;
    [SerializeField] private GameObject damageNumberPrefab;
    [SerializeField] private SpriteRenderer type0;
    [SerializeField] private SpriteRenderer type1;
    [SerializeField] private SpriteRenderer health;
    [SerializeField] private AudioClip normalAudioClip;
    [SerializeField] private AudioClip superAudioClip;
    [SerializeField] private AudioClip notGoodAudioClip;
    [SerializeField] private AudioClip noEffectAudioClip;
    private AudioSource audioSource;
    private LinkedList<GameType> _types;
    private int _hp;

    public int HP
    {
        get => _hp;
        set
        {
            if (value <= 0)
            {
                Die();
            }
            if (value > maxHp) value = maxHp;
            _hp = value;
            UpdateHealthUI((float)_hp/maxHp);
        }
    }

    void Start()
    {
        HP = maxHp;
        _types = new LinkedList<GameType>(typeSettings.GetTypeData(typeId));
        audioSource = transform.parent.GetComponent<AudioSource>();
        type0.sprite = _types.First.Value.typeIcon;
        type1.sprite = _types.First.Next?.Value.typeIcon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void UpdateHealthUI(float ratio)
    {
        if (ratio > 0.5f)
        {
            health.color = Color.green;
        }
        else if (ratio > 0.25f)
        {
            health.color = Color.yellow;
        }
        else if (ratio > 0.1f)
        {
            health.color = new Color(1f - ratio, ratio, 0);
        }
        else
        {
            health.color = Color.red;
        }

    }

    public void CreateDamageUI(int damage, EffectType effectType)
    {
        if (effectType == EffectType.NoEffect)
        {
            return;
        }
        Debug.Log($"{damage} damage");
        var numObj = Instantiate(damageNumberPrefab, GameObject.Find("DamageTextParent").transform, true);
        if (!numObj.TryGetComponent<DamageText>(out var damageText)) return;
        damageText.SetPosition(transform.position);
        damageText.Init(damage, effectType);
        
    }

    public void PlayDamageSound(EffectType effectType)
    {
        AudioClip clip = effectType switch
        {
            EffectType.Normal => normalAudioClip,
            EffectType.Super => superAudioClip,
            EffectType.NoEffect => noEffectAudioClip,
            EffectType.NotGood => notGoodAudioClip,
            _ => null
        };
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void DealDamage(SkillData attackData)
    {
        Debug.Log($"{name} is attacked by {attackData.skillName}");
        var node = _types.First;
        float effect = 1;
        int effectFlag = 0;
        while (node!=null)
        {
            effect *= CalculateEffective(attackData.typeEnum,node.Value, out var delta);
            effectFlag += delta;
            node = node.Next;
        }

        int damage;
        EffectType effectType = EffectType.Normal;
        if (effect == 0f)
        {
            Debug.Log("No effect!");
            effectType = EffectType.NoEffect;
            damage = 0;
        }
        else
        {
            if (effectFlag > 0)
            {
                Debug.Log("It's super effective!");
                effectType = EffectType.Super;
            }

            if (effectFlag < 0)
            {
                Debug.Log("It's not very effective...");
                effectType = EffectType.NotGood;
            }
            damage = (int)(attackData.power * effect);
        }

        PlayDamageSound(effectType);
        CreateDamageUI(damage, effectType);
        HP -= damage;
        

    }

    public float CalculateEffective(GameTypeEnum attackerTypeId, GameType receiverType, out int flag)
    {
        float result = 1f;
        flag = 0;
        if (receiverType.noEffectList.HasFlag(attackerTypeId))
        {
            result *= 0f;
        }
        if (receiverType.resistList.HasFlag(attackerTypeId))
        {
            result *= 0.5f;
            flag = -1;
        }
        if (receiverType.weakList.HasFlag(attackerTypeId))
        {
            result *= 2f;
            flag = 1;
        }
        return result;
    }
}

public enum EffectType
{
    NoEffect,
    NotGood,
    Normal,
    Super
}
