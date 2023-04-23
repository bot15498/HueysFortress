using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PhaseType
{
    TurnStart,
    FortressBuild,
    MainPhase,
    EndPhase,
    NoPhase
}

public class CombatManager : MonoBehaviour
{
    public Character player1;
    public Character player2;
    public Character currentPlayer;
    public PhaseType currPhase = PhaseType.NoPhase;

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
                currentPlayer.TurnReset();
                currPhase = PhaseType.MainPhase;
                if (!player1.hasFortress || !player2.hasFortress)
                {
                    currPhase = PhaseType.FortressBuild;
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
                    currPhase = PhaseType.EndPhase;
                }
                break;
            case PhaseType.EndPhase:
                // change who's turn it is
                if (currentPlayer == player1)
                {
                    currentPlayer = player2;
                }
                else
                {
                    currentPlayer = player1;
                }
                currPhase = PhaseType.TurnStart;
                break;
            case PhaseType.NoPhase:
                // If this happens something bad happeneed. 
                currPhase = PhaseType.TurnStart;
                break;
        }
    }
}
