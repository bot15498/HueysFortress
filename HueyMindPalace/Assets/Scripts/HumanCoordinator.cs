using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCoordinator : MonoBehaviour
{
    public GameObject skillsMenu;
    public GameObject wallPrefab;
    public GameObject turretPrefab;

    private Character player;
    private bool isPlacing;
    private GameObject placingObject;
    private PlacedObject placingObjectPlaced;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Character>();
        HideSkillsMenu();
        placingObject = null;
        isPlacing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing && placingObject != null)
        {
            // follow the prefab on the cursor
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            worldMousePos.z = 0f;
            worldMousePos = placingObjectPlaced.GetValidLocation(worldMousePos);
            placingObject.transform.position = worldMousePos;
            
            if (Input.GetMouseButtonDown(0))
            {
                placingObjectPlaced.EnableAbility();
                isPlacing = false;
                placingObject = null;
                placingObjectPlaced = null;
            }
        }
    }

    public void StartCoordinator()
    {
        // This is run when the player is asking what to do.
        ShowSkillsMenu();
        Camera.main.GetComponent<CameraFollow>().FollowCursor();
    }

    public void ShowSkillsMenu()
    {
        if (!skillsMenu.activeSelf)
        {
            skillsMenu.SetActive(true);
        }
        // Spawn in available buttons.
    }

    public void HideSkillsMenu()
    {
        skillsMenu.SetActive(false);
    }

    public void SendEndTurn()
    {
        HideSkillsMenu();
        player.EndTurn();
    }

    public void PlaceWall()
    {
        // Walls must be placed on the ground, so fix the x
        isPlacing = true;
        placingObject = Instantiate(wallPrefab, player.transform);
        placingObjectPlaced = placingObject.GetComponent<PlacedObject>();
    }

    public void PlaceTurret()
    {
        // turrets must be placed on walls, only one per wall.
        isPlacing = true;
        placingObject = Instantiate(turretPrefab, player.transform);
        placingObjectPlaced = placingObject.GetComponent<PlacedObject>();
    }
}
