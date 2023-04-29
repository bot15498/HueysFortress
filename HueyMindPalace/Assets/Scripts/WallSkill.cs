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
            worldMousePos = wallToPlace.GetValidLocation(worldMousePos);
            wallToPlace.transform.position = worldMousePos;

            if (Input.GetMouseButtonDown(0))
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
            }
        }
    }

    public void PlaceWall()
    {
        // Walls must be placed on the ground, so fix the x
        isPlacing = true;
        player = combat.currentPlayer;
        GameObject wallObj = Instantiate(wallPrefab, player.gameObject.transform);
        wallToPlace = wallObj.GetComponent<Wall>();
    }
}
