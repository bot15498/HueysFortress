using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerGunSkill : MonoBehaviour
{
    public bool isAiming = false;
    public LineRenderer predictPath;
    public GameObject bulletPrefab;
    public float speed = 4f;
    public int numBullets = 1;

    private CombatManager combat;
    private Character player;
    private SkillInfo skillInfo;

    // Start is called before the first frame update
    void Start()
    {
        skillInfo = GetComponent<SkillInfo>();
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if the finger gun store is available, and if it is, buff your attack


        if (isAiming)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            worldMousePos.z = 0f;

            if (Input.GetMouseButtonDown(0))
            {
                // left click, shoot
                Shoot(bulletPrefab, worldMousePos);
                skillInfo.endskillPreview(false);
                predictPath.positionCount = 0;
                isAiming = false;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // right click, cancel placement
                skillInfo.endskillPreview(true);
                predictPath.positionCount = 0;
                isAiming = false;
            }
        }
    }

    public void PreviewShoot()
    {
        player = combat.currentPlayer;
        isAiming = true;
    }

    public void Shoot(GameObject bulletPrefab, Vector3 mousepos)
    {
        // make sure the bullet has no gravity on it.
        GameObject bullet = Instantiate(bulletPrefab, player.gameObject.transform);
        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
        rb2d.velocity = mousepos.normalized * speed;
        Camera.main.GetComponent<CameraFollow>().SetTarget(bullet.transform);
    }
}
