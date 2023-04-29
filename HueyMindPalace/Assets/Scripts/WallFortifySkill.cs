using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFortifySkill : MonoBehaviour
{
    [SerializeField]
    private Wall wall = null;
    private CombatManager combat;
    private SkillInfo skillInfo;
    // Start is called before the first frame update
    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        skillInfo = GetComponent<SkillInfo>();
        // I hate this
        wall = transform.parent.parent.parent.gameObject.GetComponent<Wall>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FortifyWall()
    {
        // give wall shield. 
    }

    public void SacrificeWall()
    {

    }
}
