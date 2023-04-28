using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int maxHealth = 2;
    public int currHealth = 2;
    // This is starting velocity of the bullet. This is fixed.
    public float bulletVel = 5f;
    public GameObject bulletPrefab;

    private Vector3 targetShootVector;
    private bool isAiming = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            // calcualte angle and draw line
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 targetShootVector = worldMousePos - transform.position;
            float angle = Mathf.Atan2(targetShootVector.y, targetShootVector.x) * Mathf.Rad2Deg;
            var targetShootAngle = Quaternion.AngleAxis(angle, Vector3.forward);

            // draw prediction line

        }
    }

    public void StartAiming()
    {
        isAiming = true;
    }

    private void ShootBullet()
    {
        isAiming = false;
        GameObject bullet = Instantiate(bulletPrefab);
        // have camera follow bullet
        Camera.main.GetComponent<CameraFollow>().SetTarget(bullet.transform);
        bullet.GetComponent<Rigidbody2D>().AddForce(targetShootVector.normalized * bulletVel);
    }
}
