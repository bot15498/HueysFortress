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
    private AudioManager am;

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
                am.playclip(4, 0.5f);
                storeToPlace.EnableAbility();
                isPlacing = false;
                storeToPlace = null;

                // also go and tell the player that there is a finger gun store.
                FingerGunSkill fingerGunSkill = player.gameObject.GetComponentInChildren<FingerGunSkill>();
                fingerGunSkill.store = storeToPlace;

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
        storeObj.layer = (int)player.physicsLayer;
    }
}
