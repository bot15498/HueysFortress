using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrarySkill : MonoBehaviour
{
    // Lirbary skill is the ability to summon a library. To get more information on what the wall does, go to the library.cs script.
    public GameObject libraryPrefab;
    public bool isPlacing = false;

    private Library libraryToPlace = null;
    private CombatManager combat;
    private Character player;
    private SkillInfo skillInfo;

    // Start is called before the first frame update
    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        skillInfo = GetComponent<SkillInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing && libraryToPlace != null)
        {
            // follow the prefab on the cursor
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            worldMousePos.z = 0f;
            bool validLocation = libraryToPlace.GetValidLocation(ref worldMousePos);
            libraryToPlace.transform.position = worldMousePos;

            if (validLocation)
            {
                libraryToPlace.SetCanPlaceColor();
            }
            else
            {
                libraryToPlace.SetNoPlaceColor();
            }

            if (validLocation && Input.GetMouseButtonDown(0))
            {
                // left click, place ability. 
                libraryToPlace.EnableAbility();
                isPlacing = false;
                libraryToPlace = null;
                skillInfo.endskillPreview(false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // right click, cancel placement
                skillInfo.endskillPreview(true);
                Destroy(libraryToPlace.gameObject);
                libraryToPlace = null;
            }
        }
    }

    public void PlaceLibrary()
    {
        isPlacing = true;
        player = combat.currentPlayer;
        GameObject wallObj = Instantiate(libraryPrefab, player.gameObject.transform);
        libraryToPlace = wallObj.GetComponent<Library>();
    }
}
