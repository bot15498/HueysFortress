using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int maxFortressHealth = 5;
    public int currFortressHealth = 5;
    public int maxShieldHealth;
    public int currshieldHealth;
    public int maxMP = 3;
    public int currMp = 3;
    public bool myTurn = false;
    public bool turnDone = false;
    public bool hasFortress = false;
    public CombatManager combatManager;
    public GameObject fortressPrefab;
    public bool isReady = true;
    public List<PlacedObject> buildings = new List<PlacedObject>();
    UiManager uimanager;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI ShieldText;
    public Image healthBar;
    public Image ShieldBar;
    public GameObject shieldobject;


    private HumanCoordinator humanCoord;
    private AiCoordinator aiCoord;

    // Start is called before the first frame update
    void Start()
    {
        currFortressHealth = maxFortressHealth;
        if (GetComponent<HumanCoordinator>() != null)
        {
            humanCoord = GetComponent<HumanCoordinator>();
        }
        if (GetComponent<AiCoordinator>() != null)
        {
            aiCoord = GetComponent<AiCoordinator>();
        }
        uimanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UiManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myTurn && combatManager.currPhase == PhaseType.MainPhase)
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

        if (myTurn == true)
        {
            uimanager.updateManatext(maxMP,currMp);
            
            HealthText.text = currFortressHealth + "/" + maxFortressHealth;
        }
        
        healthBar.fillAmount = maxFortressHealth / currFortressHealth;
        if(maxShieldHealth > 0)
        {
            shieldobject.SetActive(true);
            ShieldBar.fillAmount = maxShieldHealth / currshieldHealth;
            ShieldText.text = currshieldHealth + "/" + maxShieldHealth;
        } else
        {
            shieldobject.SetActive(false);
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
       
        currMp = newMp;
    }

    public void PayMP(int mpCost)
    {
        currMp -= mpCost;
    }

    public void EndTurn()
    {
        turnDone = true;
        myTurn = false;
    }

    public void BuildBuildingAtLocation(GameObject prefab, Vector3 worldPos)
    {

    }
}
