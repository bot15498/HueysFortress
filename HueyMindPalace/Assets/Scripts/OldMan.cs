using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : MonoBehaviour
{
    public int damage= 3;
    public float moveSpeed = 5f;

    private PolygonCollider2D collider2d;
    private SpriteRenderer sprite;
    private CombatManager combat;
    private AudioManager am;
    private Character owner;

    // Start is called before the first frame update
    void Start()
    {
        collider2d = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        LayerMask groundmask = LayerMask.GetMask("Ground");
        am = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        if (combat == null)
        {
            // assume on creation, that the owner is whoever turn it is. 
            combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
            owner = combat.currentPlayer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colliderObj = collision.collider.gameObject;
        if (colliderObj.tag == "Building")
        {
            if (colliderObj.GetComponent<PlacedObject>() != null)
            {
                PlacedObject obj = colliderObj.GetComponent<PlacedObject>();
                obj.TakeDamage(damage);
            }
            else if (colliderObj.GetComponent<Fortress>() != null)
            {
                Fortress fort = colliderObj.GetComponent<Fortress>();
                fort.owner.TakeDamage(damage);
                fort.StartFlashRed();
            }
            Camera.main.GetComponent<CameraFollow>().FollowCursor();
            Destroy(this.gameObject);
        }
    }
}
