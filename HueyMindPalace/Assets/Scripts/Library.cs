using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Library : MonoBehaviour, PlacedObject
{
    public Character owner;
    public int mpIncrease = 2;
    public int maxHealth = 2;
    public int currHealth = 2;
    public int maxShieldHealth = 0;
    public int currshieldHealth = 0;

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
    public bool isPlaced { get => _isPlaced; set => _isPlaced = value; }
    public bool lastPlaced { get => _lastPlaced; set => _lastPlaced = value; }

    // Start is called before the first frame update
    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        LayerMask groundmask = LayerMask.GetMask("Ground");
        DisableAbility();
        if (combat == null)
        {
            // assume on creation, that the owner is whoever turn it is. 
            combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
            owner = combat.currentPlayer;
        }

        // When a library is built, increase max mana.
        owner.maxMP += mpIncrease;
        owner.currMp += mpIncrease;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableAbility()
    {
        // when something isn't placed, disable a bunch of stuff.
        isPlaced = false;
        box2d.enabled = false;
        SetCanPlaceColor();
    }

    public void EnableAbility()
    {
        isPlaced = true;
        box2d.enabled = true;
        sprite.color = new Color(1, 1, 1);
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
        sprite.color = new Color(0, 1, 0);
    }

    public void SetNoPlaceColor()
    {
        sprite.color = new Color(1, 0, 0);
    }

    public bool GetValidLocation(ref Vector3 worldpos)
    {
        int groundmask = 1 << 6;
        Vector3 dir = (new Vector3(0, -1, 0));
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 10, 0), dir, Mathf.Infinity, groundmask);
        //Debug.DrawRay(transform.position + new Vector3(0, 10, 0), dir * dist, Color.green);
        if (hit.collider.gameObject.tag == "Ground")
        {
            worldpos.y = hit.point.y;

        }
        else if (hit.collider.gameObject.tag != "Building")
        {
            return false;
        }

        return true;
    }
}
