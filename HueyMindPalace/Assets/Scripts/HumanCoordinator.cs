using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanCoordinator : MonoBehaviour
{
    public GameObject skillsMenu;
    public GameObject wallPrefab;
    public GameObject turretPrefab;


    private Character player;
    private bool isPlacing;
    private bool iscurrentTurn;
    private GameObject placingObject;
    private PlacedObject placingObjectPlaced;
    private bool SkillsOpen;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Character>();
        HideSkillsMenu();
        placingObject = null;
        isPlacing = false;
        iscurrentTurn = false;
        SkillsOpen = false;
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
        iscurrentTurn = true;
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

    private void OnMouseDown()
    {
        if(iscurrentTurn == true)
        {
            toggleSkillUI();
        }
       
    }

    private void toggleSkillUI()
    {
        if (SkillsOpen == false)
        {
            skillsMenu.SetActive(true);
            skillsMenu.GetComponent<SkillsAnimation>().openMenu();
            SkillsOpen = true;
        }else if(SkillsOpen == true)
        {

            skillsMenu.GetComponent<SkillsAnimation>().closeMenu();
            //skillsMenu.SetActive(false);
            SkillsOpen = false;
        }

        
    }
}
