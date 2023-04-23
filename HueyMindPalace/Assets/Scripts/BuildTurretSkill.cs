using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTurretSkill : MonoBehaviour, ISkill
{
    public int cost = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanUse()
    {
        if(GetComponent<BuildWallSkill>().isBuilt)
        {
            return true;
        }
        return false;
    }
}
