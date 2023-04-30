using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerGunStoreEnhanceSkill : MonoBehaviour
{
    [SerializeField]
    private FingerGunStore store = null;
    private CombatManager combat;
    private SkillInfo skillInfo;

    // Start is called before the first frame update
    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        skillInfo = GetComponent<SkillInfo>();
        // I hate this
        store = transform.parent.parent.parent.gameObject.GetComponent<FingerGunStore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyMultishot()
    {
        store.hasMultishot = true;
        skillInfo.endskillPreview(false);
    }

    public void BuyIncreaseDamage()
    {
        store.damageIncrease += 1;
        skillInfo.endskillPreview(false);
    }

    public void BuyReduceCooldown()
    {
        store.hasReduceCooldown = true;
        skillInfo.endskillPreview(false);
    }
}
