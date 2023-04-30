using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Child : MonoBehaviour
{
    // a check for if we know it's our turn or not. 
    public bool isThinking = false;
    public int maxWalls = 2;
    public int maxTurrets = 2;
    public GameObject skillsMenu;
    // walls
    public List<Wall> walls;
    public WallSkill wallSkill;
    public float minWallBuildDist = 10f;
    public float maxWallBuildDist = 20f;
    private SkillInfo wallSkillInfo;
    // trains
    public TrainSkill trainSkill;
    private SkillInfo trainSkillInfo;
    // shoot
    public DodgeballSkill shootSkill;
    public float shootSpeed = 5f;
    private SkillInfo shootSkillInfo;

    private Character myChar;
    private CombatManager combat;
    private bool openedSkillsMenu = false;
    [SerializeField]
    private Fortress opponentFortress;
    // Start is called before the first frame update
    void Start()
    {
        myChar = GetComponent<Character>();
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();

        wallSkillInfo = wallSkill.GetComponent<SkillInfo>();
        trainSkillInfo = trainSkill.GetComponent<SkillInfo>();
        shootSkillInfo = shootSkill.GetComponent<SkillInfo>();
        opponentFortress = combat.player1.fort;
    }

    // Update is called once per frame
    void Update()
    {
        if(opponentFortress == null)
        {
            opponentFortress = combat.player1.fort;
        }
        if (!openedSkillsMenu)
        {
            skillsMenu.GetComponent<SkillsAnimation>().openMenu();
            openedSkillsMenu = true;
        }
        // wait until it's my turn.
        if (myChar.isReady && myChar.myTurn && combat.currPhase == PhaseType.MainPhase && !isThinking)
        {
            // in a very ugly fashion, check which walls are gone now.
            List<Wall> newWalls = new List<Wall>();
            foreach(Wall wall in walls)
            {
                if(wall != null)
                {
                    newWalls.Add(wall);
                }
            }
            walls = newWalls;

            StartCoroutine(DoTurn());
            isThinking = true;
        }
    }

    private IEnumerator DoTurn()
    {
        // very dumb ai.
        bool doingAction = true;
        bool placedWallThisTurn = false;
        // let the child think for a second
        yield return new WaitForSeconds(1f);
        while (doingAction)
        {
            if (false)
            {
                // if train skill is available, use it.
            }
            else if (walls.Select(x => x.turret != null).Count() < walls.Count)
            {
                // if wall doesn't have a turret, build one if you have mp and off cooldown
            }
            else if (!placedWallThisTurn && walls.Count < maxWalls && wallSkillInfo.CanUseSkill())
            {
                // build a new wall if you have mp and off cooldown. 
                float xdiff = Random.Range(minWallBuildDist, maxWallBuildDist);
                Vector3 buildpos = transform.position;
                buildpos.x -= xdiff;
                Wall newWall = wallSkill.PlaceWall(buildpos);
                walls.Add(newWall);

                // trigger the skill info stuff.
                wallSkillInfo.ActivateSkillPreview();
                wallSkillInfo.endskillPreview(false);

                // wait a bit before making it a real wall.
                yield return new WaitForSeconds(0.1f);
                newWall.EnableAbility();

                // Follwo the wall.
                Camera.main.GetComponent<CameraFollow>().SetTarget(newWall.transform);
                yield return new WaitForSeconds(1f);
                placedWallThisTurn = true;
            }
            else if (false)
            {
                // shoot with turret if you can
            }
            else if (false)
            {
                // fortify wall if you can.
            }
            else if (shootSkillInfo.CanUseSkill())
            {
                // shoot with child if you can.
                GameObject ball =  shootSkill.Shoot(opponentFortress.gameObject.transform, shootSpeed);

                // trigger the skill info stuff.
                shootSkillInfo.ActivateSkillPreview();
                shootSkillInfo.endskillPreview(false);

                // Follow the ball
                Camera.main.GetComponent<CameraFollow>().SetTarget(ball.transform);
                yield return new WaitForSeconds(3f);
            }
            else
            {
                // out of things to do
                doingAction = false;
            }
            yield return null;
        }

        // at the end.
        myChar.EndTurn();
        isThinking = false;
        yield return null;
    }
}
