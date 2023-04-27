using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSkill : MonoBehaviour, ISkill
{
    public int cost = 1;

    private Character player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CanUse()
    {
        return player.currMp >= cost;
    }

    public void Use()
    {
        // hid skill menu on parent.
        //player.HideSkillsMenu();
        // spawn arm in.
        // subtract MP from character.
        player.SetCurrMp(player.currMp - cost);
    }
}
