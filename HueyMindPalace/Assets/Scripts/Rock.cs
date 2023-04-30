using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
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
        //Debug.Log(collision.collider.gameObject.tag);
        if(collision.collider.gameObject.tag == "Ground")
        {
            Camera.main.GetComponent<CameraFollow>().FollowCursor();
            Destroy(this.gameObject);
        }
    }
}
