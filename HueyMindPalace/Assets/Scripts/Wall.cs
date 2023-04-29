using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Wall : MonoBehaviour, PlacedObject
{
    public int cost = 2;
    public GameObject turretPrefab;
    public float turretXcoord;
    public float turretYcoord;
    public int maxHealth = 2;
    public int currHealth = 2;
    public Turret turret;

    private bool _isPlaced = false;
    private bool _lastPlaced = false;
    private BoxCollider2D box2d;
    private SpriteRenderer sprite;
    private CombatManager combat;
    private Character owner;

    public bool isPlaced { get => _isPlaced; set => _isPlaced=value; }
    public bool lastPlaced { get => _lastPlaced; set => _lastPlaced=value; }

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
    }

    // Update is called once per frame
    void Update()
    {
        // Only enable functionality once. 
        if (!isPlaced && lastPlaced != isPlaced)
        {
            EnableAbility();
        }
        lastPlaced = isPlaced;
    }

    public void DisableAbility()
    {
        // when something isn't placed, disable a bunch of stuff.
        isPlaced = false;
        box2d.enabled = false;
        sprite.color = new Color(0, 1, 0);
    }

    public void EnableAbility()
    {
        isPlaced = true;
        box2d.enabled = true;
        sprite.color = new Color(1, 1, 1);
    }

    public Vector3 GetValidLocation(Vector3 worldpos)
    {
        // Clamp the wall to be at y=0
        Vector3 fixedPos = worldpos;

        int groundmask = 1 << 6;
        Vector3 dir = (new Vector3(0, -1, 0));
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0,10,0), dir, Mathf.Infinity,groundmask);
        //Debug.DrawRay(transform.position + new Vector3(0, 10, 0), dir * dist, Color.green);
        if(hit.collider.gameObject.tag == "Ground")
        {
            //fixedPos.y = hit.point.y;
            // Debug.Log(hit.point.y);
            fixedPos.y = hit.point.y;
            
        }
        else if(hit.collider.gameObject.tag != "Ground")
        {
            //fixedPos.y = hit.point.y;
        }



        //fixedPos.y = 0;
        // TODO check for overlaps here

        return fixedPos;
    }

    public void BuildTurret()
    {
        // for building a turret on top of this wall.
        // the turret will always be in the same position
        GameObject turretObj = Instantiate(turretPrefab, transform);
        turretObj.transform.position = new Vector3(turretXcoord, turretYcoord);
        turret = turretObj.GetComponent<Turret>();
    }

    public void ShootTurret()
    {
        // Tell the turret to start aiming, and then handle the 
        if (turret != null)
        {
            turret.StartAiming();
        }
    }
}
