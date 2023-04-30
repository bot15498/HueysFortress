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
    public int numLinePoints = 1500;
    public FingerGunStore store;

    private CombatManager combat;
    private Character player;
    private SkillInfo skillInfo;
    public GameObject gunarm;
    AudioManager am;
    Animator mananimation;

    // Start is called before the first frame update
    void Start()
    {
        skillInfo = GetComponent<SkillInfo>();
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        am = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        mananimation = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UiManager>().manAnimation;
    }

    // Update is called once per frame
    void Update()
    {
        // check if the finger gun store is available, and if it is, do things accordingly. 


        if (isAiming)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            worldMousePos.z = 0f;

            // draw path
            Vector3 diff = worldMousePos - player.gameObject.transform.position;
            /*Vector2[] trajectory = PredictPath(bulletPrefab, speed, diff, player.gameObject.transform.position, numLinePoints);
            predictPath.positionCount = trajectory.Length;
            Vector3[] trajectory3d = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
            {
                trajectory3d[i] = trajectory[i];
            }
            predictPath.SetPositions(trajectory3d);*/

            if (Input.GetMouseButtonDown(0))
            {
                // left click, shoot

                am.playclip(0, 0.5f);
                Shoot(bulletPrefab, diff);
                skillInfo.endskillPreview(false);
                predictPath.positionCount = 0;
                if (store != null && store.hasReduceCooldown)
                {
                    // Tick down cooldown twice. 
                    skillInfo.maxCooldown -= 1;
                    skillInfo.currentCooldown -= 1;
                }

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
        mananimation.Play("ManGun");
        gunarm.SetActive(true);
        isAiming = true;
    }

    public void Shoot(GameObject bulletPrefab, Vector3 mousepos)
    {
        // make sure the bullet has no gravity on it.
        GameObject bullet = Instantiate(bulletPrefab, player.gameObject.transform);
        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
        // rotate to match direction
        float angle = Mathf.Atan2(mousepos.y, mousepos.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        rb2d.SetRotation(targetRotation);
        // fix physics.
        bullet.layer = (int)player.physicsLayer;
        rb2d.velocity = mousepos.normalized * speed;
        // follow bullet until it hits something.
        gunarm.SetActive(false);
        mananimation.Play("ManIsIdle");
        // check damage
        Dodgeball finger = bullet.GetComponent<Dodgeball>();
        if(store != null)
        {
            finger.damage += store.damageIncrease;
        }
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
