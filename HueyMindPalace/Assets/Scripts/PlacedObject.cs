using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlacedObject
{
    public bool isPlaced { get; set; }
    public bool lastPlaced { get; set; }

    public void EnableAbility();
    public void DisableAbility();
    public bool GetValidLocation(ref Vector3 worldpos);
}
