using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text label;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition += new Vector2(0, 1);
    }

    public void Init(int damage,EffectType effectType)
    {
        label.color = effectType switch
        {
            EffectType.Normal => Color.white,
            EffectType.NotGood => Color.gray,
            EffectType.Super => Color.red,
            _ => label.color
        };
        label.text = $"-{damage}";
        StartCoroutine(WaitToDestroy(0.5f));
    }

    public void SetPosition(Vector3 worldPosition)
    {
        rectTransform.position = Camera.main!.WorldToScreenPoint(worldPosition);
    }

    IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
