using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Child : MonoBehaviour
{
    // a check for if we know it's our turn or not. 
    public bool isThinking = false;
    public int maxWalls = 2;
    public int maxTurrets = 2;
    public List<Wall> walls;

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
        // wait until it's my turn.
        if (myChar.isReady && myChar.myTurn && !isThinking)
        {
            StartCoroutine(DoTurn());
            isThinking = true;
        }
    }

    private IEnumerator DoTurn()
    {
        // very dumb ai.
        bool doingAction = true;
        while (doingAction)
        {
            if (false)
            {
                // if train skill is available, use it.
            }
            else if (walls.Select(x => x.turret != null).Count() < walls.Count)
            {
                // if wall doesn't have a turret, build one if you have mp and off cooldown
            }
            else if (walls.Count < maxWalls && combat.turnNumber > 3)
            {
                // build a new wall if you have mp and off cooldown. 
            }
            else if (false)
            {
                // shoot with turret if you can
            }
            else if (false)
            {
                // shoot with child if you can.
            }
            else
            {
                // out of things to do
                doingAction = false;
            }
            yield return null;
        }

        // at the end.
        myChar.EndTurn();
        isThinking = false;
        yield return null;
    }
}
