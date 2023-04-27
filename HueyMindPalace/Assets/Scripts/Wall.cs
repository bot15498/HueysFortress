using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, PlacedObject
{
    public int cost = 2;
    public Turret turret;

    private bool _isPlaced = false;
    private bool _lastPlaced = false;
    private BoxCollider2D box2d;
    private SpriteRenderer sprite;

    public bool isPlaced { get => _isPlaced; set => _isPlaced=value; }
    public bool lastPlaced { get => _lastPlaced; set => _lastPlaced=value; }

    // Start is called before the first frame update
    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        DisableAbility();
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
        fixedPos.y = 0;
        // TODO check for overlaps here

        return fixedPos;
    }
}
