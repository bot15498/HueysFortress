using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int damage = 2;
    public Rigidbody2D rb2d;
    private CombatManager combat;
    private AudioManager am;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        am = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colliderObj = collision.collider.gameObject;
        //Debug.Log(collision.collider.gameObject.tag);
        if (colliderObj.tag == "Ground")
        {
            Camera.main.GetComponent<CameraFollow>().FollowCursor();
            Destroy(this.gameObject);
        }
        if (colliderObj.tag == "Building")
        {
            if(colliderObj.GetComponent<PlacedObject>() != null)
            {
                PlacedObject obj = colliderObj.GetComponent<PlacedObject>();
                obj.TakeDamage(damage);
                am.playclip(7, 0.5f);
            }
            else if (colliderObj.GetComponent<Fortress>() != null)
            {
                Fortress fort = colliderObj.GetComponent<Fortress>();
                fort.owner.TakeDamage(damage);
                fort.StartFlashRed();
                am.playclip(6, 0.5f);
            }
            //Camera.main.GetComponent<CameraFollow>().FollowCursor();
            Camera.main.GetComponent<CameraFollow>().StartGoBackToPlayer();
            Destroy(this.gameObject);
        }
        if (colliderObj.GetComponent<OldMan>() != null)
        {
            Camera.main.GetComponent<CameraFollow>().StartGoBackToPlayer();
            am.playclip(7, 0.5f);
            Destroy(colliderObj);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator GoBackToPlayer()
    {
        Camera.main.GetComponent<CameraFollow>().SetTarget(combat.currentPlayer.transform);
        yield return new WaitForSeconds(2f);
        Camera.main.GetComponent<CameraFollow>().FollowCursor();
        yield return null;
    }
}
