using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    private float currentHealth = 0;
    public int coin = 100;
    public Transform target; 


    private NavMeshAgent agent;


    public Canvas canvas;
    public Image healthBar;


    private void Awake()
    {
        canvas.worldCamera = Camera.main;
        target = GameObject.FindGameObjectWithTag("End").transform;

        currentHealth = health;

        healthBar.fillAmount = currentHealth / health;
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
        currentHealth -= amount;

        healthBar.fillAmount = currentHealth / health;

        if (currentHealth <= 0f)
        {
            GameManager.Instance.money += coin;
            GameManager.Instance.kills += 1;
            Destroy(gameObject);
        }
    }
}
