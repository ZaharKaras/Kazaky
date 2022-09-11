using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyUnit eU = collision.gameObject.GetComponent<EnemyUnit>();
        if(eU != null)
        {
            eU.TakeDamage(25);
            Destroy(gameObject);
        }
    }

    public void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}
