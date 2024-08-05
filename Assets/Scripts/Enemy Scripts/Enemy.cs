using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public int coin = 100;
    public Transform target; 

    private NavMeshAgent agent;


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("End").transform;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            GameManager.Instance.money += coin;
            GameManager.Instance.kills += 1;
            Destroy(gameObject);
        }
    }
}
