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
    public Vector2 fortressSpawnLocation;
    public bool isReady = true;
    public List<PlacedObject> buildings = new List<PlacedObject>();
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
        
        healthBar.fillAmount = currFortressHealth / maxFortressHealth;
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

    public void BuildFortress()
    {
        // Builds the initial fortress.
        Instantiate(fortressPrefab, fortressSpawnLocation, Quaternion.identity);
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

    public void TakeDamage(int damage)
    {
        currFortressHealth = Mathf.Max(currFortressHealth - damage, 0);
    }
}
