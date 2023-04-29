using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gremlin : MonoBehaviour
{
    public int damage = 2;

    private CombatManager combat;
    private Character owner;

    // Start is called before the first frame update
    void Start()
    {
        if (combat == null)
        {
            // assume on creation, that the owner is whoever turn it is. 
            combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
            owner = combat.currentPlayer;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(combat.currentPlayer != owner)
        {
            // walk forward.
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Character")
        {
            collision.gameObject.GetComponent<Character>().TakeDamage(damage);
        }
    }
}
