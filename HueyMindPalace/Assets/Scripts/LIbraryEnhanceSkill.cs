using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LIbraryEnhanceSkill : MonoBehaviour
{
    public int manaIncrease = 2;



    [SerializeField]
    private Library library = null;
    private CombatManager combat;
    private SkillInfo skillInfo;
    private Character owner;
    Animator mananimation;

    // Start is called before the first frame update
    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        skillInfo = GetComponent<SkillInfo>();
        // I hate this
        library = transform.parent.parent.parent.gameObject.GetComponent<Library>();
        mananimation = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UiManager>().manAnimation;
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

        mananimation.Play("manBigBrain");
        Camera.main.GetComponent<CameraFollow>().SetTarget(owner.transform);
        StartCoroutine(delay());
        owner.maxMP += manaIncrease;
        owner.currMp += manaIncrease;
        skillInfo.endskillPreview(false);
    }


    IEnumerator delay()
    {

        yield return new WaitForSeconds(2);
        mananimation.Play("ManIsIdle");
        Camera.main.GetComponent<CameraFollow>().FollowCursor();

    }
}
