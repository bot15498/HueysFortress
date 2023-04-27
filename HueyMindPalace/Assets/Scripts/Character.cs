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

    private HumanCoordinator humanCoord;
    private AiCoordinator aiCoord;

    // Start is called before the first frame update
    void Start()
    {
        currFortressHealth = maxFortressHealth;
        mpTextBox.text = string.Format("Current MP: {0}/{1}", currMp, maxMP);
        if (GetComponent<HumanCoordinator>() != null)
        {
            humanCoord = GetComponent<HumanCoordinator>();
        }
        if (GetComponent<AiCoordinator>() != null)
        {
            aiCoord = GetComponent<AiCoordinator>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myTurn)
        {
            // ask the coordinator what to do, they probably know.
            if (humanCoord != null)
            {
                humanCoord.StartCoordinator();
            }
            else if (aiCoord != null)
            {
                aiCoord.StartCoordinator();
            }
        }
    }

    public void BuildFortress(float x, float y)
    {
        // Builds the initial fortress.
        Instantiate(fortressPrefab, new Vector3(x, y), Quaternion.identity);
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

    public void EndTurn()
    {
        turnDone = true;
        myTurn = false;
    }

    public void BuildBuildingAtLocation(GameObject prefab, float xval)
    {

    }
}
