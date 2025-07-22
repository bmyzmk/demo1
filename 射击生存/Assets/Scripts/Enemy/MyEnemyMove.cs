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

        //�ж��Ƿ������������������ٽ��е������������
        if (!myEnemyHealth.IsDeath&&!myPlayerHealth.IsPlayerDeath)
        {
            //����׷��Ŀ�ĵ�Ϊ��ɫλ��
            nav.SetDestination(Player.transform.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
