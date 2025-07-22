using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyAttack : MonoBehaviour
{
    private GameObject Player;
    private MyPlayerHealth myPlayerHealth;
    private Animator EnemyAni;

    private bool OnAttack=false;
    public int EnemyAttackDamage = 10;
    private float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {   

        Player = GameObject.FindWithTag("Player");
        myPlayerHealth = Player.GetComponent<MyPlayerHealth>();
        //调用敌人的animator组件，而不是player.getcompoment<animator>();
        EnemyAni = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!myPlayerHealth.IsPlayerDeath&&OnAttack && timer>2.5f)
        {
            Attack();
            timer = 0;
        }
        if (myPlayerHealth.IsPlayerDeath) 
        { 
            EnemyAni.SetTrigger("IsIdle"); 
        }
        
        
    }

    private void Attack()
    {
        myPlayerHealth.TakenDamage(EnemyAttackDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            OnAttack = true;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
          OnAttack=false;
        }
    }
}
