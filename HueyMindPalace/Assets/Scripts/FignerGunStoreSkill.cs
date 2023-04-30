using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FignerGunStoreSkill : MonoBehaviour
{
    // this is for building the finger gun store.
    public GameObject storePrefab;
    public bool isPlacing = false;

    private FingerGunStore storeToPlace = null;
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
        if (isPlacing && storeToPlace != null)
        {
            // follow the prefab on the cursor
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            worldMousePos.z = 0f;
            bool validLocation = storeToPlace.GetValidLocation(ref worldMousePos);
            storeToPlace.transform.position = worldMousePos;

            if(validLocation)
            {
                storeToPlace.SetCanPlaceColor();
            }
            else
            {
                storeToPlace.SetNoPlaceColor();
            }

            if (validLocation && Input.GetMouseButtonDown(0))
            {
                // left click, place ability. 
                storeToPlace.EnableAbility();
                isPlacing = false;
                storeToPlace = null;
                skillInfo.endskillPreview(false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // right click, cancel placement
                skillInfo.endskillPreview(true);
                Destroy(storeToPlace.gameObject);
                storeToPlace = null;
            }
        }
    }

    public void PlaceStore()
    {
        isPlacing = true;
        player = combat.currentPlayer;
        GameObject storeObj = Instantiate(storePrefab, player.gameObject.transform);
        storeToPlace = storeObj.GetComponent<FingerGunStore>();
    }
}
