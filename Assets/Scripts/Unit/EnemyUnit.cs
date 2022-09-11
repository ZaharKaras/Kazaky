using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyUnit : MonoBehaviour
{
    private NavMeshAgent navAgent;
    public UnitStatType.Base stats;

    private Collider[] rangeColliders;
    private Transform aggroTarget;
    private PlayerUnit aggroUnit;
    private bool hasAggro;
    private float aggroDistance;

    private Vector3 keyPosition;
    private float keyDistance;

    public GameObject unitStatDisplay;
    public Image healthBarAmount;
    public float currentHealth;

    private float attackCoolDown;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        hasAggro = false;
        keyPosition = transform.position;
        currentHealth = stats.hp + stats.armor;
    }

    private void Update()
    {
        attackCoolDown -= Time.deltaTime;

        if (!hasAggro)
        {
            CheckEnemy();
        }
        else
        {
            MoveToAggro();
        }
    }

    private void LateUpdate()
    {
        HandleHealth();
    }

    public void MoveUnit(Vector3 destination)
    {
        navAgent.SetDestination(destination);
        keyPosition = destination;
    }

    private void CheckEnemy()
    {
        rangeColliders = Physics.OverlapSphere(transform.position, stats.aggroRange);

        for (int i = 0; i < rangeColliders.Length; i++)
        {
            if (rangeColliders[i].gameObject.layer == UnitHandler.instance.pUnitLayer)
            {
                aggroTarget = rangeColliders[i].transform;
                aggroUnit = aggroTarget.gameObject.GetComponent<PlayerUnit>();
                hasAggro = true;
                break;
            }
        }
    }

    private void MoveToAggro()
    {
        if (aggroTarget == null)
        {
            navAgent.SetDestination(transform.position);
            hasAggro = false;
        }
        else
        {
            aggroDistance = Vector3.Distance(transform.position, aggroUnit.transform.position);
            keyDistance = Vector3.Distance(transform.position, keyPosition);

            if (keyDistance <= stats.aggroRange)
            {
                if (aggroDistance > stats.attackRange)
                {
                    navAgent.SetDestination(aggroUnit.transform.position);
                }
                else if (aggroDistance <= stats.attackRange)
                {
                    navAgent.SetDestination(transform.position);
                    Attack();
                }
            }
            else
            {
                navAgent.SetDestination(keyPosition);
                hasAggro = false;
            }
        }
    }

    private void HandleHealth()
    {
        Camera camera = Camera.main;

        unitStatDisplay.transform.LookAt(unitStatDisplay.transform.position +
            camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

        healthBarAmount.fillAmount = currentHealth / (stats.hp + stats.armor);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        InputHandler.instance.selectedUnits.Remove(gameObject.transform);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void Attack()
    {
        if(attackCoolDown <= 0 && aggroDistance <= stats.aggroRange + 1)
        {
            aggroUnit.TakeDamage(stats.damage);
            attackCoolDown = stats.attackSpeed;
        }
    }
}
