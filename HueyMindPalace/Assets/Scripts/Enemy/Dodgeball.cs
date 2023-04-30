using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    public int damage = 1;
    public Rigidbody2D rb2d;
    private AudioManager am;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        am = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colliderObj = collision.collider.gameObject;
        if (colliderObj.tag == "Ground")
        {
            Camera.main.GetComponent<CameraFollow>().StartGoBackToPlayer();
            Destroy(this.gameObject);
        }
        if (colliderObj.tag == "Building")
        {
            if (colliderObj.GetComponent<PlacedObject>() != null)
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
                am.playclip(6, 0.25f);
            }
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
}
