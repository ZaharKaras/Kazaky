using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerUnit : MonoBehaviour
{
    private NavMeshAgent navAgent;
    public UnitStatType.Base stats;

    private Collider[] rangeColliders;
    public Transform aggroTarget;
    public EnemyUnit aggroUnit;
    public bool hasAggro;
    private float aggroDistance;

    public GameObject unitStatDisplay;
    public Image healthBarAmount;
    public float currentHealth;

    private float attackCoolDown;

    public bool isSelected;
    private Vector3 mouseHitPoint;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        hasAggro = false;
        currentHealth = stats.hp + stats.armor;
        mouseHitPoint = transform.position;
    }

    private void Update()
    {
        attackCoolDown -= Time.deltaTime;
        if(!isSelected)
        {
            if (!hasAggro)
            {
                CheckEnemy();
            }
            else
            {
                MoveToAggro();
            }
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(mouseHitPoint, transform.position) <= 1.5)
            isSelected = false;
    }

    private void LateUpdate()
    {
        HandleHealth();
    }

    public void MoveUnit(Vector3 destination)
    {
        navAgent.SetDestination(destination);
        isSelected = true;
        mouseHitPoint = destination;
    }

    private void CheckEnemy()
    {
        rangeColliders = Physics.OverlapSphere(transform.position, stats.aggroRange);

        for(int i = 0; i< rangeColliders.Length; i++)
        {
            if (rangeColliders[i].gameObject.layer == UnitHandler.instance.eUnitLayer)
            {
                aggroTarget = rangeColliders[i].transform;
                aggroUnit = aggroTarget.gameObject.GetComponent<EnemyUnit>();
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
        if (attackCoolDown <= 0 && aggroDistance <= stats.aggroRange + 1)
        {
            aggroUnit.TakeDamage(stats.damage);
            attackCoolDown = stats.attackSpeed;
        }
    }
}
