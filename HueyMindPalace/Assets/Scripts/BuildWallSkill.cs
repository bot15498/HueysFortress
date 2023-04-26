using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWallSkill : MonoBehaviour, ISkill
{
    public int cost = 2;
    // Boolean to say if the object has been "placed" in the world or not.
    public bool isBuilt = false;
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
        return !isBuilt;
    }
}
