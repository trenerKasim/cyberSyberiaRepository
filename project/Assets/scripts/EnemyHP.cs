using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHP : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp = 50;
    [SerializeField]
    private int currenthp = 0;
    public bool alive = true;
    [SerializeField]
    private float speed = 0;
    
    [SerializeField]
    private int agroRange = 0;
    [SerializeField]
    private int shootRange = 0;

    [SerializeField]
    private weapon weapon;

    [SerializeField]
    private GameObject weaponModel;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform bulletSpawnPoint;
    [SerializeField]
    private float fireTimer;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private int scoreReward;
    [SerializeField]
    private scoreCounter counter;

    void Start()
    {
        alive = true;
        currenthp = hp;
		agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame

    public void TakeDamage(int amount)
    {
        currenthp -= amount;

        if (currenthp <= 0)
        {
            currenthp = 0;
            alive = false;
            counter.addToScore(scoreReward);
            Destroy(this.gameObject);
        }
    }
	void Update()
	{
        if(Vector3.Distance(transform.position, target.position) > agroRange)
		{
            if (fireTimer > 0)
            {
                fireTimer -= Time.deltaTime;
            }
            return;
		}
        Vector3 lokPos = target.position - this.transform.position;
        lokPos.y = 0;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(lokPos), Time.deltaTime * 3);
        agent.speed = speed;
        agent.SetDestination(target.position);
        if(Vector3.Distance(transform.position, target.position) > shootRange)
        { return; }
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
        else
        {
            GameObject.Instantiate(weapon.BulletsPrefab, bulletSpawnPoint.transform.position, this.transform.rotation);
            fireTimer = weapon.FireRate;
        };
    }
}

   



