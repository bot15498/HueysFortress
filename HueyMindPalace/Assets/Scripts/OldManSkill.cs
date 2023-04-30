using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManSkill : MonoBehaviour
{
    public GameObject oldManPrefab;

    private Wall wallToPlace = null;
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
        
    }

    public OldMan PlaceOldMan(Vector3 location)
    {
        GameObject oldManObj = Instantiate(oldManPrefab, transform.position, Quaternion.identity);
        oldManObj.transform.position = location;
        return oldManObj.GetComponent<OldMan>();
    }
}
