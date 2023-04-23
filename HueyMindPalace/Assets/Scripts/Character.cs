using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxFortressHealth = 5;
    public int currFortressHealth = 5;
    public int maxMP = 3;
    public int currMp = 3;
    public bool myTurn = false;
    public bool turnDone = false;
    public bool hasFortress = false;
    public GameObject fortressPrefab;
    public bool isReady = true;
    public TextMeshProUGUI mpTextBox;
    public GameObject skillsMenu;

    // Start is called before the first frame update
    void Start()
    {
        currFortressHealth = maxFortressHealth;
        mpTextBox.text = string.Format("Current MP: {0}/{1}", currMp, maxMP);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void BuildFortress()
    {
        Instantiate(fortressPrefab);
        Debug.Log("Creating fortress");
        hasFortress = true;
    }

    public void TurnReset()
    {
        // repleniosh MP
        currMp = maxMP;
        turnDone = false;
        myTurn = true;
    }

    public void SetCurrMp(int newMp)
    {
        mpTextBox.text = string.Format("Current MP: {0}/{1}", currMp, maxMP);
        currMp = newMp;
    }

    public void ShowSkillsMenu()
    {
        skillsMenu.SetActive(true);
        // Spawn in available buttons.
    }

    public void HideSkillsMenu()
    {
        skillsMenu.SetActive(false);
    }

    public void EndTurn()
    {
        HideSkillsMenu();
        turnDone = true;
        myTurn = false;
    }
}
