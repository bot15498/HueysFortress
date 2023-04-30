using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int damage = 2;
    public Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
