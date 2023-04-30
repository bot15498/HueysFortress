using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnhanceSkill : MonoBehaviour
{
    public GameObject manTurretPrefab;
    public GameObject bulletPrefab;
    public bool isAiming = false;
    public float maxForceScale = 20f;
    public int numLinePoints = 1500;
    public LineRenderer predictPath;

    private Turret turret = null;
    private CombatManager combat;
    private SkillInfo skillInfo;
    private Character owner;

    // Start is called before the first frame update
    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        skillInfo = GetComponent<SkillInfo>();
        // I hate this
        turret = transform.parent.parent.parent.gameObject.GetComponent<Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming && turret.hasManTurret)
        {
            // draw the path.
            // The x location of the mouse is where you want the rock to land
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            worldMousePos.z = 0f;

            // draw path from the man turret location
            Vector3 spawnLoc = turret.manTurretLocation;
            Vector3 diff = worldMousePos - spawnLoc;
            diff.y = diff.y * 2;
            Vector2[] trajectory = PredictPath(bulletPrefab, maxForceScale * diff.y, diff, spawnLoc, numLinePoints);
            predictPath.positionCount = trajectory.Length;
            Vector3[] trajectory3d = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
            {
                trajectory3d[i] = trajectory[i];
            }
            predictPath.SetPositions(trajectory3d);

            if (Input.GetMouseButtonDown(0))
            {
                // left click, shoot
                Shoot(diff, maxForceScale * diff.y, spawnLoc);
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

    public void BuildManTurret()
    {
        GameObject manTurret = Instantiate(manTurretPrefab, turret.gameObject.transform.position, Quaternion.identity);
        manTurret.transform.SetParent(turret.gameObject.transform);
        Vector3 pos = turret.gameObject.transform.position;
        pos.x += turret.manTurretLocation.x;
        pos.y += turret.manTurretLocation.y;
        manTurret.transform.position = pos;
        Debug.Log(manTurret.transform.position);

        owner = combat.currentPlayer;
        manTurret.layer = (int)owner.physicsLayer;

        turret.hasManTurret = true;
        turret.buildManTurretButton.SetActive(false);
        turret.shootManTurretButton.SetActive(true);
        skillInfo.endskillPreview(false);
    }

    public void PreviewShootManTurret()
    {
        owner = combat.currentPlayer;
        isAiming = true;
    }

    private void Shoot(Vector3 direction, float velocity, Vector3 spawnLoc)
    {
        GameObject bullet = Instantiate(bulletPrefab, turret.gameObject.transform);
        bullet.transform.position = spawnLoc;
        bullet.layer = (int)owner.physicsLayer;
        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
        rb2d.velocity = direction.normalized * velocity;
        Camera.main.GetComponent<CameraFollow>().SetTarget(bullet.transform);
    }

    public Vector2[] PredictPath(GameObject prefab, float velocity, Vector3 diff, Vector3 offset, int steps)
    {
        Rigidbody2D rb2d = prefab.GetComponent<Rigidbody2D>();
        Vector2[] results = new Vector2[steps];

        Vector2 currPos = offset;

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravity = Physics2D.gravity * rb2d.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rb2d.drag;
        Vector2 movestep = velocity * timestep * diff.normalized;

        for (int i = 0; i < steps; i++)
        {
            movestep += gravity;
            movestep *= drag;
            currPos += movestep;
            results[i] = currPos;
        }

        return results;
    }
}
