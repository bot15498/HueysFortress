using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : MonoBehaviour
{
    public int damage= 3;
    public float moveSpeed = 5f;

    private SpriteRenderer sprite;
    private CombatManager combat;
    private AudioManager am;
    private Character owner;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
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
        // slowly move towards opponent. 
        Vector3 pos = transform.position;
        if (combat.currentPlayer != owner && combat.currPhase == PhaseType.MainPhase)
        {
            pos.x -= Time.deltaTime* moveSpeed;
        }        
        
        int groundmask = 1 << 6;
        Vector3 dir = (new Vector3(0, -1, 0));
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 10, 0), dir, Mathf.Infinity, groundmask);
        if (hit.collider.gameObject.tag == "Ground")
        {
            //fixedPos.y = hit.point.y;
            // Debug.Log(hit.point.y);
            pos.y = hit.point.y;
        }
        transform.position = pos;
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
            am.playclip(8, 0.5f);
            Destroy(this.gameObject);
        }
    }
}
