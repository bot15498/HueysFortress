using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour
{
    // script to put on the character that is controlled by the player (not an ai).
    public GameObject skillsMenu;

    private Character myChar;
    private CombatManager combat;
    private bool enableFollow = false;
    private bool skillsOpen = false;
    AudioManager am;
    // Start is called before the first frame update
    void Start()
    {
        myChar = GetComponent<Character>();
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        am = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myChar.myTurn && combat.currPhase == PhaseType.MainPhase && !enableFollow)
        {
            Camera.main.GetComponent<CameraFollow>().FollowCursor();
            enableFollow = true;
        }
        if (!myChar.myTurn)
        {
            enableFollow = false;
        }
    }

    private void OnMouseDown()
    {
        if(myChar.myTurn)
        {
            ToggleSkillUi();
            am.playclip(5, 0.25f);
        }
    }

    private void ToggleSkillUi()
    {
        if (skillsOpen == false)
        {
            skillsMenu.SetActive(true);
            skillsMenu.GetComponent<SkillsAnimation>().openMenu();
            skillsOpen = true;
        }
        else if (skillsOpen == true)
        {
            skillsMenu.GetComponent<SkillsAnimation>().closeMenu();
            skillsOpen = false;
        }
    }
}
