using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FingerGunStore : MonoBehaviour, PlacedObject
{
    public Character owner;
    public bool hasMultishot = false;
    public int damageIncrease = 0;
    public int cooldownReduction = 0;
    public int maxHealth = 2;
    public int currHealth = 2;
    public int maxShieldHealth = 0;
    public int currshieldHealth = 0;
    public GameObject skillsMenu;

    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI ShieldText;
    public Image healthBar;
    public Image ShieldBar;
    public GameObject shieldobject;
    public GameObject healthCanvas;

    private bool _isPlaced = false;
    private bool _lastPlaced = false;
    private BoxCollider2D box2d;
    private SpriteRenderer sprite;
    private CombatManager combat;
    private bool skillsOpen = false;
    AudioManager am;

    public bool isPlaced { get => _isPlaced; set => _isPlaced = value; }
    public bool lastPlaced { get => _lastPlaced; set => _lastPlaced = value; }

    // Start is called before the first frame update
    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        am = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        LayerMask groundmask = LayerMask.GetMask("Ground");
        DisableAbility();
        if (combat == null)
        {
            // assume on creation, that the owner is whoever turn it is. 
            combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
            owner = combat.currentPlayer;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HealthText.text = currHealth + "/" + maxHealth;

        healthBar.fillAmount = (float)currHealth / maxHealth;
        if (maxShieldHealth > 0)
        {
            shieldobject.SetActive(true);
            ShieldBar.fillAmount = (float)maxShieldHealth / currshieldHealth;
            ShieldText.text = currshieldHealth + "/" + maxShieldHealth;
        }
        else
        {
            shieldobject.SetActive(false);
        }
    }


    public void DisableAbility()
    {
        // when something isn't placed, disable a bunch of stuff.
        isPlaced = false;
        box2d.enabled = false;
        SetCanPlaceColor();
        healthCanvas.SetActive(false);
    }

    public void EnableAbility()
    {
        isPlaced = true;
        box2d.enabled = true;
        sprite.color = new Color(1, 1, 1);
        healthCanvas.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        if (currshieldHealth - damage > 0)
        {
            currshieldHealth -= damage;
        }
        else
        {
            currshieldHealth = 0;
            maxShieldHealth = 0;
            currHealth = Mathf.Max(currHealth - damage, 0);
        }

        if (currHealth == 0)
        {
            // KILL
            Destroy(this.gameObject);
        }
    }

    public void SetCanPlaceColor()
    {
        if (sprite != null)
        {
            sprite.color = new Color(0, 1, 0);
        }
    }

    public void SetNoPlaceColor()
    {
        if (sprite != null)
        {
            sprite.color = new Color(1, 0, 0);
        }
    }

    public bool GetValidLocation(ref Vector3 worldpos)
    {
        int groundmask = 1 << 6;
        Vector3 dir = (new Vector3(0, -1, 0));
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 10, 0), dir, Mathf.Infinity, groundmask);
        //Debug.DrawRay(transform.position + new Vector3(0, 10, 0), dir * dist, Color.green);
        if (hit.collider.gameObject.tag == "Ground")
        {
            //fixedPos.y = hit.point.y;
            // Debug.Log(hit.point.y);
            worldpos.y = hit.point.y;

        }
        if (worldpos.x > -3.02f && worldpos.x < 0)
        {
            return false;
        }
        return true;
    }

    private void OnMouseDown()
    {
        if (owner.myTurn)
        {
            ToggleSkillUi();
            am.playclip(5, 0.25f);
        }
    }

    private void ToggleSkillUi()
    {
        if (skillsOpen == false)
        {
            skillsMenu.SetActive(true);
            skillsMenu.GetComponent<SkillsAnimation>().openMenu();
            skillsOpen = true;
        }
        else if (skillsOpen == true)
        {
            skillsMenu.GetComponent<SkillsAnimation>().closeMenu();
            skillsOpen = false;
        }
    }
}
