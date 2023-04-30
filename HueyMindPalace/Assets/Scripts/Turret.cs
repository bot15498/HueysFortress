using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour, PlacedObject
{
    public float bulletVel = 5f;
    public int maxHealth = 2;
    public int currHealth = 2;
    public int maxShieldHealth = 0;
    public int currshieldHealth = 0;
    public Character owner;
    public GameObject skillsMenu;
    public Vector2 manTurretLocation;
    public bool hasManTurret = false;

    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI ShieldText;
    public Image healthBar;
    public Image ShieldBar;
    public GameObject shieldobject;
    public GameObject healthCanvas;
    public GameObject buildManTurretButton;
    public GameObject shootManTurretButton;

    private bool _isPlaced = false;
    private bool _lastPlaced = false;
    private PolygonCollider2D collider2d;
    private SpriteRenderer sprite;
    private CombatManager combat;
    private bool skillsOpen;

    public bool isPlaced { get => _isPlaced; set => _isPlaced = value; }
    public bool lastPlaced { get => _lastPlaced; set => _lastPlaced = value; }

    void Start()
    {
        collider2d = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        LayerMask groundmask = LayerMask.GetMask("Ground");
        DisableAbility();
        if (combat == null)
        {
            // assume on creation, that the owner is whoever turn it is. 
            combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
            owner = combat.currentPlayer;
        }
        buildManTurretButton.SetActive(true);
        shootManTurretButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
        collider2d.enabled = false;
        SetCanPlaceColor();
        healthCanvas.SetActive(false);
    }

    public void EnableAbility()
    {
        isPlaced = true;
        collider2d.enabled = true;
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
        // prevent spamming.
        if (sprite != null)
        {
            sprite.color = new Color(0, 1, 0);
        }
    }

    public void SetNoPlaceColor()
    {
        // prevent spamming.
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

        // if you can't place, return false

        return true;
    }

    private void OnMouseDown()
    {
        if (owner.myTurn)
        {
            ToggleSkillUi();
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
