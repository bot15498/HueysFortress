using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSkill : MonoBehaviour
{
    // skill for summoning the train, only used by the child.
    public GameObject trainPrefab;
    public List<Vector2> spawnPoints;
    public float spawnDelay = 0.5f;
    public float trainSpeed = 6f;
    public bool isDone = true;

    private CombatManager combat;
    private Character player;
    private SkillInfo skillInfo;
    void Start()
    {
        skillInfo = GetComponent<SkillInfo>();
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        // always going to be player 2 ( I think)?
        player = combat.player2;
    }

    void Update()
    {

    }

    public void StartSummonTrains()
    {
        StartCoroutine(SummonTrains());
    }

    public IEnumerator SummonTrains()
    {
        isDone = false;
        foreach (Vector2 spawnpoint in spawnPoints)
        {
            // calculate random angle between +/- 10 deg
            float angle = Random.Range(-20f, 20f) - 90;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 velocity = new Vector3(trainSpeed * Mathf.Cos(angle * Mathf.Deg2Rad), trainSpeed * Mathf.Sin(angle * Mathf.Deg2Rad));
            Debug.Log(velocity);

            // spawn train with velocity
            Vector3 pos = spawnpoint;
            GameObject train = Instantiate(trainPrefab, pos, Quaternion.identity);
            Rigidbody2D rb2d = train.GetComponent<Rigidbody2D>();
            train.layer = (int)player.physicsLayer;
            rb2d.velocity = velocity;
            rb2d.SetRotation(targetRotation);
            yield return new WaitForSeconds(spawnDelay);
        }
        isDone = true;
        yield return null;
    }
}
