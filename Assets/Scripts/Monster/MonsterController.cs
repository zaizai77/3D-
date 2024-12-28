using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterStatus { GUARD,PATROL,CHASE,DEAD}
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Character_Status))]
public class MonsterController : MonoBehaviour,IEndGameObserve
{
    private MonsterStatus monsterStatus;
    private NavMeshAgent agent;

    [Header("Basic settings")]
    public float sightRadius;
    public GameObject attackTarget;
    public float lookAtTime;
    public float lastAttackTime = -1;
    public float remainLookAtTime;
    public bool isGuard;
    public float speed;
    public Animator anim;
    public Character_Status character_Status;

    bool isWalk;
    bool isChases;
    bool isFollow;

    [Header("Patrol State")]
    public float patrolRange;
    private Vector3 wayPoints;
    private Vector3 guardPos;
    private Quaternion guardRotation;
    private bool isDead = false;
    private Collider coll;
    private bool playerDead = false;
    protected AudioSource audioSource;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        anim = GetComponent<Animator>();
        guardPos = transform.position;
        remainLookAtTime = lookAtTime;
        character_Status = GetComponent<Character_Status>();
        guardRotation = transform.rotation;
        coll = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if(isGuard)
        {
            monsterStatus = MonsterStatus.GUARD;
        } 
        else
        {
            monsterStatus = MonsterStatus.PATROL;
            GetNewWayPoint();
        }
        GameManager.Instance.AddObserver(this);
    }

    //切换场景时启用
    //void OnEnable()
    //{
    //    GameManager.Instance.AddObserver(this);
    //}

    void OnDisable()
    {
        if (!GameManager.IsInitialized)
            return;
        GameManager.Instance.RemoveObesrver(this);
    }

    void Update()
    {
        if(character_Status.CurrentHealth == 0)
        {
            isDead = true;
        }
        if(!playerDead)
        {
            SwitchStatus();
            SwitchAnimation();
            lastAttackTime -= Time.deltaTime;
        }
    }

    void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChases);
        anim.SetBool("Follow", isFollow);
        anim.SetBool("Critical", character_Status.isCritical);
        anim.SetBool("Death", isDead);
    }

    void SwitchStatus()
    {
        if (isDead)
        {
            monsterStatus = MonsterStatus.DEAD;
        }
        else if (FoundPlayer())
        {
            monsterStatus = MonsterStatus.CHASE;
        }
        switch (monsterStatus)
        {
            case MonsterStatus.GUARD:
                isChases = false;
                if(transform.position != guardPos)
                {
                    isWalk = true;
                    agent.isStopped = false;
                    agent.destination = guardPos;

                    if(Vector3.SqrMagnitude(guardPos - transform.position) <= agent.stoppingDistance)
                    {
                        isWalk = false;
                        transform.rotation = Quaternion.Lerp(transform.rotation, guardRotation, 0.01f);
                    }
                }
                break;
            case MonsterStatus.PATROL:
                isChases = true;
                agent.speed = speed * 0.5f;
                if(Vector3.Distance(wayPoints,transform.position) <= agent.stoppingDistance)
                {
                    isWalk = false;
                    if(remainLookAtTime > 0)
                    {
                        remainLookAtTime -= Time.deltaTime;
                    } else
                    {
                        GetNewWayPoint();
                    }
                } else
                {
                    isWalk = true;
                    agent.destination = wayPoints;
                }
                break;
            case MonsterStatus.CHASE:
                isWalk = false;
                isChases = true;
                agent.speed = speed;
                if(!FoundPlayer())
                {
                    if(remainLookAtTime < 0)
                    {
                        agent.destination = transform.position;
                        remainLookAtTime -= Time.deltaTime;
                    }
                    else if(isGuard)
                    {
                        monsterStatus = MonsterStatus.GUARD;
                    }
                    else
                    {
                        monsterStatus = MonsterStatus.PATROL;
                    }
                    isFollow = false;
                } 
                else
                {
                    isFollow = true;
                    agent.isStopped = false;
                    agent.destination = attackTarget.transform.position;
                }
                if(TargetInAttackRange() || TargetInSkillRange())
                {
                    isFollow = false;
                    agent.isStopped = true;
                    if(lastAttackTime < 0)
                    {
                        lastAttackTime = character_Status.attackData.coolDown;
                        character_Status.isCritical = Random.value < character_Status.attackData.criticalChance;
                        Attack();
                    }
                    //Debug.Log(lastAttackTime);
                }
                break;
            case MonsterStatus.DEAD:
                audioSource.Play();
                coll.enabled = false;

                agent.radius = 0;
                Destroy(gameObject, 0.5f);
                break;
            default:
                break;
        }
    }

    void Attack()
    {
        transform.LookAt(attackTarget.transform);
        if(TargetInAttackRange())
        {
            anim.SetTrigger("Attack");
            Debug.Log("attack");
        }
        if(TargetInSkillRange())
        {
            anim.SetTrigger("Skill");
            Debug.Log("skill");
        }
    }

    bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        foreach (var target in colliders)
        {
            if(target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                return true;
            }
        }
        attackTarget = null;
        return false;
    }

    bool TargetInAttackRange()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position) <= character_Status.attackData.attackRange;
        }
        else
        {
            return false;
        }
    }

    bool TargetInSkillRange()
    {
        if(attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position) <= character_Status.attackData.skillRange;
        } else
        {
            return false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    void GetNewWayPoint()
    {
        remainLookAtTime = lookAtTime;
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomZ = Random.Range(-patrolRange, patrolRange);

        Vector3 randomPoint = new Vector3(guardPos.x + randomX, transform.position.y, guardPos.z + randomZ);
        NavMeshHit hit;
        wayPoints = NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1) ? hit.position : transform.position;
    }

    void Hit()
    {
        if(attackTarget != null && transform.IsFaceingTarget(attackTarget.transform))
        {
            var targetStats = attackTarget.GetComponent<Character_Status>();
            targetStats.TakeDamage(character_Status, targetStats);
        }
    }

    public void EndNotify()
    {
        isChases = false;
        isWalk = false;
        attackTarget = null;
        anim.SetBool("Win", true);
        playerDead = true;
    }
}
