using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFortifySkill : MonoBehaviour
{
    public int shieldAmount = 2;

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
        wall.maxShieldHealth += shieldAmount;
        wall.currshieldHealth += shieldAmount;
        skillInfo.endskillPreview(false);
    }

    public void SacrificeWall()
    {
        wall.owner.maxShieldHealth += wall.currHealth;
        wall.owner.currshieldHealth += wall.currHealth;
        Destroy(wall.gameObject);
        skillInfo.endskillPreview(false);
    }
}
