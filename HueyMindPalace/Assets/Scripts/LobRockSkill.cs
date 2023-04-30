using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobRockSkill : MonoBehaviour
{
    public GameObject rockPrefab;
    public bool isAiming = false;
    public float maxForceScale = 1f;
    public LineRenderer predictPath;
    public int numLinePoints = 50;

    private CombatManager combat;
    private Character player;
    private SkillInfo skillInfo;
    private AudioManager am;

    // Start is called before the first frame update
    void Start()
    {
        skillInfo = GetComponent<SkillInfo>();
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        am = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            // draw the path.
            // The x location of the mouse is where you want the rock to land
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            worldMousePos.z = 0f;

            // draw path
            Vector3 diff = worldMousePos - player.gameObject.transform.position;
            diff.y = diff.y * 2;
            Vector2[] trajectory = PredictPath(rockPrefab, maxForceScale * diff.y, diff, player.gameObject.transform.position, numLinePoints);
            predictPath.positionCount = trajectory.Length;
            Vector3[] trajectory3d = new Vector3[trajectory.Length];
            for(int i=0; i<trajectory.Length; i++)
            {
                trajectory3d[i] = trajectory[i];
            }
            predictPath.SetPositions(trajectory3d);

            if (Input.GetMouseButtonDown(0))
            {
                // left click, shoot
                am.playclip(2, 0.5f);
                Shoot(diff, maxForceScale * diff.y);
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

    public void PreviewRockSkill()
    {
        player = combat.currentPlayer;
        isAiming = true;
    }

    private void Shoot(Vector3 diff, float velocity)
    {
        GameObject rock = Instantiate(rockPrefab, player.gameObject.transform);
        rock.layer = (int)player.physicsLayer;
        Rigidbody2D rb2d = rock.GetComponent<Rigidbody2D>();
        rb2d.velocity = diff.normalized * velocity;
        Camera.main.GetComponent<CameraFollow>().SetTarget(rock.transform);
    }

    private float CalcualteAngle(Vector3 startPos, Vector3 mouseWorldPos, float force, Rock rock)
    {
        float xdiff = mouseWorldPos.x - startPos.x;
        float ydiff = mouseWorldPos.y - startPos.y;
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        float gravity = (Physics2D.gravity * rock.rb2d.drag).y;
        // solve for theta based on force and time.
        Mathf.Acos(xdiff);
        return 0f;
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
