using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeballSkill : MonoBehaviour
{
    // only used by the child.
    public GameObject dodgeballPrefab;

    private CombatManager combat;
    private Character player;
    private SkillInfo skillInfo;

    // Start is called before the first frame update
    void Start()
    {
        skillInfo = GetComponent<SkillInfo>();
        combat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatManager>();
        player = skillInfo.playercharacter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Shoot(Transform target, float velocity)
    {
        GameObject ball = Instantiate(dodgeballPrefab, player.transform);
        ball.layer = (int)player.physicsLayer;

        Rigidbody2D rb2d = ball.GetComponent<Rigidbody2D>();
        Vector3 diff = target.position - player.transform.position;
        rb2d.velocity = diff.normalized * velocity;

        return ball;
    }
}
