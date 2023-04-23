using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArm : MonoBehaviour
{
    public float smooth; 
    private Quaternion targetRotation;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        targetRotation = transform.rotation;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rb2d.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, 10 * smooth * Time.deltaTime));
    }
}
