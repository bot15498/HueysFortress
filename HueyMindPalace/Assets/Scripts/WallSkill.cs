using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSkill : MonoBehaviour
{
    // Wall skill is the ability to summon a wall. To get more information on what the wall does, go to the wall.cs script.
    public GameObject wallPrefab;
    public bool isPlacing = false;

    private Wall wallToPlace = null;
    private CombatManager combat;
    private Character player;
    private SkillInfo skillInfo;

    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        skillInfo = GetComponent<SkillInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing && wallToPlace != null)
        {
            // follow the prefab on the cursor
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            worldMousePos.z = 0f;
            bool validLocation = wallToPlace.GetValidLocation(ref worldMousePos);
            wallToPlace.transform.position = worldMousePos;

            if (validLocation)
            {
                wallToPlace.SetCanPlaceColor();
            }
            else
            {
                wallToPlace.SetNoPlaceColor();
            }

            if (validLocation && Input.GetMouseButtonDown(0))
            {
                // left click, place ability. 
                wallToPlace.EnableAbility();
                isPlacing = false;
                wallToPlace = null;
                skillInfo.endskillPreview(false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // right click, cancel placement
                skillInfo.endskillPreview(true);
                Destroy(wallToPlace.gameObject);
                wallToPlace = null;
            }
        }
    }

    public void PlaceWall()
    {
        // Walls must be placed on the ground, so fix the x
        isPlacing = true;
        player = combat.currentPlayer;
        GameObject wallObj = Instantiate(wallPrefab, player.gameObject.transform);
        wallObj.layer = (int)player.physicsLayer;
        wallToPlace = wallObj.GetComponent<Wall>();
    }

    public Wall PlaceWall(Vector3 location)
    {
        // place the wall without thinking. 
        isPlacing = false;
        player = combat.currentPlayer;
        GameObject wallObj = Instantiate(wallPrefab, player.gameObject.transform);
        wallToPlace = wallObj.GetComponent<Wall>();
        wallObj.layer = (int)player.physicsLayer;

        bool validLocation = wallToPlace.GetValidLocation(ref location);
        // dangerou piece of code that just keeps trying to find a spot. 
        wallToPlace.transform.position = location;
        wallToPlace = wallObj.GetComponent<Wall>();
        wallToPlace = null;
        return wallObj.GetComponent<Wall>();
    }
}
