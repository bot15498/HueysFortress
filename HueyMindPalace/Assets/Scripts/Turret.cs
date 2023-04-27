using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, PlacedObject
{
    public int cost = 2;

    private bool _isPlaced = false;
    private bool _lastPlaced = false;
    private SpriteRenderer sprite;

    public bool isPlaced { get => _isPlaced; set => _isPlaced = value; }
    public bool lastPlaced { get => _lastPlaced; set => _lastPlaced = value; }

    void Start()
    {
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
        sprite.color = new Color(0, 1, 0);
    }

    public void EnableAbility()
    {
        isPlaced = true;
        sprite.color = new Color(1, 1, 1);
    }

    public Vector3 GetValidLocation(Vector3 worldpos)
    {
        // turrets can only be placed on walls.
        Transform closestWall = FindClosestWall(worldpos);
        float width = closestWall.GetComponent<SpriteRenderer>().bounds.size.x;
        float height = closestWall.GetComponent<SpriteRenderer>().bounds.size.y;
        worldpos.x = Mathf.Clamp(worldpos.x, closestWall.position.x - width / 2, closestWall.position.x + width / 2);
        worldpos.y = closestWall.position.y + height;
        worldpos.z = 0f;
        return worldpos;
    }

    private Transform FindClosestWall(Vector3 worldpos)
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject shortestWall = null;
        float shortestDist = 99999f;
        foreach (GameObject wall in walls)
        {
            float currDistance = Vector3.Distance(wall.transform.position, worldpos);
            if (currDistance < shortestDist)
            {
                shortestWall = wall;
                shortestDist = currDistance;
            }
        }
        return shortestWall.transform;
    }
}
