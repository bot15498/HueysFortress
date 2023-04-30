using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSkill : MonoBehaviour
{
    // Turret skill is the ability to summon a turret. Look at Turret.cs for information about the turrets
    public GameObject turretPrefab;
    public bool isPlacing = false;

    private Turret turretToPlace = null;
    private CombatManager combat;
    private Character player;
    private SkillInfo skillInfo;
    AudioManager am;

    // Start is called before the first frame update
    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        am = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        skillInfo = GetComponent<SkillInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing && turretToPlace != null)
        {
            // follow the prefab on the cursor
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            worldMousePos.z = 0f;
            bool validLocation = turretToPlace.GetValidLocation(ref worldMousePos);
            turretToPlace.transform.position = worldMousePos;

            if (validLocation)
            {
                turretToPlace.SetCanPlaceColor();
            }
            else
            {
                turretToPlace.SetNoPlaceColor();
            }

            if (validLocation && Input.GetMouseButtonDown(0))
            {
                // left click, place ability. 
                am.playclip(4, 0.5f);
                turretToPlace.EnableAbility();
                isPlacing = false;
                turretToPlace = null;
                skillInfo.endskillPreview(false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // right click, cancel placement
                skillInfo.endskillPreview(true);
                Destroy(turretToPlace.gameObject);
                turretToPlace = null;
            }
        }
    }

    public void PlaceTurret()
    {
        // Walls must be placed on the ground, so fix the x
        isPlacing = true;
        player = combat.currentPlayer;
        GameObject wallObj = Instantiate(turretPrefab, player.gameObject.transform);
        wallObj.layer = (int)player.physicsLayer;
        turretToPlace = wallObj.GetComponent<Turret>();
    }

    public Turret PlaceTurret(Vector3 location)
    {
        // place the wall without thinking. 
        isPlacing = false;
        player = combat.currentPlayer;
        GameObject turretObj = Instantiate(turretPrefab, player.gameObject.transform);
        turretToPlace = turretObj.GetComponent<Turret>();
        turretObj.layer = (int)player.physicsLayer;

        bool validLocation = turretToPlace.GetValidLocation(ref location);
        // dangerou piece of code that just keeps trying to find a spot. 
        turretToPlace.transform.position = location;
        turretToPlace = turretObj.GetComponent<Turret>();
        turretToPlace = null;
        return turretObj.GetComponent<Turret>();
    }
}
