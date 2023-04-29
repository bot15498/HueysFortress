using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    private Character myChar;
    private CombatManager combat;
    // Start is called before the first frame update
    void Start()
    {
        myChar = GetComponent<Character>();
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myChar.isReady && myChar.myTurn && combat.currPhase == PhaseType.MainPhase)
        {
            myChar.EndTurn();
        }
    }
}
