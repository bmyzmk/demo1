using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyMove : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent nav;
    private MyEnemyHealth myEnemyHealth;
    private MyPlayerHealth myPlayerHealth;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        myEnemyHealth = GetComponent<MyEnemyHealth>();
        myPlayerHealth=Player.GetComponent<MyPlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

        //判断是否死亡，若死亡，不再进行导航坐标点设置
        if (!myEnemyHealth.IsDeath&&!myPlayerHealth.IsPlayerDeath)
        {
            //设置追逐目的地为角色位置
            nav.SetDestination(Player.transform.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
