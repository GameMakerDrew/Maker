using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    public float range = 20f;
    public float attackSpeed;

    public GameObject moveable;
    public GameObject projectile;
    public GameObject barrelEnd;

    private GameObject[] ShootablesArray;
    private GameObject target;

    enum MoveState
    {
        Moving,
        Idle
    }

    enum AttackState
    {
        Attacking,
        Idle
    }

    MoveState playerMovingState = MoveState.Idle;
    AttackState playerAttackState = AttackState.Idle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetShootables();
        StateChanger();
        StartShooting();

        if (playerMovingState == MoveState.Idle)
        {
            GetClosestShootable();
            RotateTowardsTarget();
        }
    }

    public void GetShootables()
    {
        ShootablesArray =  GameObject.FindGameObjectsWithTag("Shootable");
    }

    public void GetClosestShootable()
    {
        if (ShootablesArray.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            int index = 0;
            int closest = 0;

            foreach (GameObject shootable in ShootablesArray)
            {
                float testDist = Vector3.Distance(shootable.transform.position, moveable.transform.position);
                if (testDist < closestDistance)
                {
                    closestDistance = testDist;
                    closest = index;
                }
                index++;
            }
               target = ShootablesArray[closest];
        }
        else
        {
            StopShooting();
        }
    }

    public void RotateTowardsTarget()
    {
        if (target && Vector3.Distance(target.transform.position, moveable.transform.position) < range) {
            moveable.transform.rotation = Quaternion.LookRotation(target.transform.position - moveable.transform.position);
        }
        else
        {
            StopShooting();
        }
    }

    public void StartShooting()
    {
        if (target && playerAttackState == AttackState.Idle && playerMovingState == MoveState.Idle && Vector3.Distance(target.transform.position, moveable.transform.position) < range)
        {
            playerAttackState = AttackState.Attacking;
            InvokeRepeating("InstantiateProjectile", 0f, attackSpeed);
        }
    }

    public void StopShooting()
    {
        CancelInvoke("InstantiateProjectile");
        playerAttackState = AttackState.Idle;
    }

    private void InstantiateProjectile()
    {
        moveable.GetComponent<AudioSource>().Play();
        GameObject proj = Instantiate(projectile, barrelEnd.transform.position, barrelEnd.transform.rotation);
    }

    public void StateChanger()
    {
        if (moveable.GetComponent<NavMeshAgent>().hasPath)
        {
            StopShooting();
            playerAttackState = AttackState.Idle;
            playerMovingState = MoveState.Moving;

        }
        else
        {
            playerMovingState = MoveState.Idle;
        }


    }
}
