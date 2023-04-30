using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PhaseType
{
    TurnStart,
    FortressBuild,
    MainPhase,
    ActionPhase,
    EndPhase,
    NoPhase
}

public class CombatManager : MonoBehaviour
{
    public Character player1;
    public Character player2;
    public Character currentPlayer;
    public PhaseType currPhase = PhaseType.NoPhase;
    public int turnNumber = 1;

    private float timer = 0f;

    void Start()
    {
        // conditions to start the match/
        // Just move on and set player 1 to go first for now.
        currPhase = PhaseType.TurnStart;
        currentPlayer = player1;
    }

    void Update()
    {
        // main loop to handle phases each loop
        HandlePhase();
    }

    private void HandlePhase()
    {
        switch (currPhase)
        {
            case PhaseType.TurnStart:
                Camera.main.GetComponent<CameraFollow>().SetTarget(currentPlayer.transform);
                currentPlayer.TurnReset();
                // wait some amount of time before moving on
                timer += Time.deltaTime;
                if (timer > 1f)
                {
                    currPhase = PhaseType.MainPhase;
                    currentPlayer.myTurn = true;
                    if (!player1.hasFortress || !player2.hasFortress)
                    {
                        currPhase = PhaseType.FortressBuild;
                    }
                    timer = 0f;
                }
                break;
            case PhaseType.FortressBuild:
                // some animation triggering?
                if(!player1.hasFortress)
                {
                    player1.BuildFortress();
                }
                if (!player2.hasFortress)
                {
                    player2.BuildFortress();
                }
                if(currentPlayer.isReady)
                {
                    currPhase = PhaseType.MainPhase;
                }
                break;
            case PhaseType.MainPhase:
                if (currentPlayer.turnDone)
                {
                    currPhase = PhaseType.ActionPhase;
                }
                break;
            case PhaseType.ActionPhase:
                currPhase = PhaseType.EndPhase;
                break;
            case PhaseType.EndPhase:
                // change who's turn it is
                if (currentPlayer == player1)
                {
                    currentPlayer = player2;
                    TickAllSkillInfo(player2);
                }
                else
                {
                    currentPlayer = player1;
                    TickAllSkillInfo(player1);
                    turnNumber++;
                }
                currPhase = PhaseType.TurnStart;
                break;
            case PhaseType.NoPhase:
                // If this happens something bad happeneed. 
                currPhase = PhaseType.TurnStart;
                break;
        }
    }

    private void TickAllSkillInfo(Character player)
    {
        foreach(SkillInfo skill in player.skillInfos)
        {
            skill.tickCooldown();
        }
    }
}
