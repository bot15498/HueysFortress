using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PhaseType
{
    TurnStart,
    FortressBuild,
    MainPhase,
    ActionPhase,
    EndPhase,
    NoPhase,
    Player1Wins,
    Player2Wins
}

public class CombatManager : MonoBehaviour
{
    public Character player1;
    public Character player2;
    public Character currentPlayer;
    public PhaseType currPhase = PhaseType.NoPhase;
    public int turnNumber = 1;
    Animator mananimation;

    private float timer = 0f;

    void Start()
    {
        // conditions to start the match/
        // Just move on and set player 1 to go first for now.
        currPhase = PhaseType.TurnStart;
        currentPlayer = player1;
        mananimation = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UiManager>().manAnimation;
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
                if (timer > 0.8f)
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
                if (player1.fort == null)
                {
                    GameObject fort = player1.BuildFortress();
                    fort.layer = 7; // player 1 layer
                }
                if (player2.fort == null)
                {
                    GameObject fort = player2.BuildFortress();
                    fort.layer = 8; // player 2 layer
                }
                if (currentPlayer.isReady)
                {
                    currPhase = PhaseType.MainPhase;
                }
                break;
            case PhaseType.MainPhase:
                if (currentPlayer.turnDone)
                {
                    currPhase = PhaseType.ActionPhase;
                }
                if(player1.currFortressHealth <= 0)
                {
                    currPhase = PhaseType.Player2Wins;
                }
                if (player2.currFortressHealth <= 0)
                {
                    currPhase = PhaseType.Player1Wins;
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
            case PhaseType.Player1Wins:
                Camera.main.GetComponent<CameraFollow>().SetTarget(player1.transform);
                StartCoroutine(TransitionToWinnerScene(2));
                break;
            case PhaseType.Player2Wins:
                Camera.main.GetComponent<CameraFollow>().SetTarget(player2.transform);
                StartCoroutine(TransitionToWinnerScene(3));
                break;
        }
    }

    private void TickAllSkillInfo(Character player)
    {
        foreach (SkillInfo skill in player.skillInfos)
        {
            if (skill != null)
            {
                skill.tickCooldown();
            }
        }
    }

    private IEnumerator TransitionToWinnerScene(int index)
    {
        Camera.main.GetComponent<CameraFollow>().SetTarget(player1.transform);
        mananimation.Play("ManWin");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(index);
    }
}
