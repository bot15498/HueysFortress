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
    }

    public void BuyIncreaseDamage()
    {
        store.hasIncreaseDamage = true;
    }
}
