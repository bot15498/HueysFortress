using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIbraryEnhanceSkill : MonoBehaviour
{
    public int manaIncrease = 2;

    [SerializeField]
    private Library library = null;
    private CombatManager combat;
    private SkillInfo skillInfo;
    private Character owner;

    // Start is called before the first frame update
    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        skillInfo = GetComponent<SkillInfo>();
        // I hate this
        library = transform.parent.parent.parent.gameObject.GetComponent<Library>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void IncreaseMaxMana()
    {
        if (owner == null)
        {
            owner = combat.currentPlayer;
        }
        owner.maxMP += manaIncrease;
        owner.currMp += manaIncrease;
        skillInfo.endskillPreview(false);
    }
}
