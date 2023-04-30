using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerPhysicsType
{
    Player1 = 7,
    Player2 = 8
}

public class Character : MonoBehaviour
{
    public PlayerPhysicsType physicsLayer;
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
    public Fortress fort;
    public Vector2 fortressSpawnLocation;
    public bool isReady = true;
    UiManager uimanager;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI ShieldText;
    public Image healthBar;
    public Image ShieldBar;
    public GameObject shieldobject;
    public List<SkillInfo> skillInfos = new List<SkillInfo>();

    // Start is called before the first frame update
    void Start()
    {
        uimanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UiManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myTurn == true)
        {
            uimanager.updateManatext(maxMP,currMp);
            
            HealthText.text = currFortressHealth + "/" + maxFortressHealth;
        }
        
        healthBar.fillAmount = (float)currFortressHealth / maxFortressHealth;
        if(maxShieldHealth > 0)
        {
            shieldobject.SetActive(true);
            ShieldBar.fillAmount = (float)maxShieldHealth / currshieldHealth;
            ShieldText.text = currshieldHealth + "/" + maxShieldHealth;
        } else
        {
            shieldobject.SetActive(false);
        }
    }

    public GameObject BuildFortress()
    {
        // Builds the initial fortress.
        GameObject fortObj = Instantiate(fortressPrefab, fortressSpawnLocation, Quaternion.identity);
        fortObj.GetComponent<Fortress>().owner = this;
        fort = fortObj.GetComponent<Fortress>();
        return fortObj;
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

    public void TakeDamage(int damage)
    {
        if(currshieldHealth - damage > 0)
        {
            currshieldHealth -= damage;
        }
        else
        {
            currshieldHealth = 0;
            maxShieldHealth = 0;
            currFortressHealth = Mathf.Max(currFortressHealth - damage, 0);
        }

        if (currFortressHealth == 0)
        {
            // YOU LOSE
        }
    }
}
