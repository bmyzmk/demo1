using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyManager : MonoBehaviour
{   
    public GameObject Enemy;
    public float FirstCreateTime=0;
    public float CreatEnemyTime = 3f;
    public GameObject CreateEnemyPoint;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", FirstCreateTime,CreatEnemyTime);//InvokeRepeating(����������ʼʱ�䣬�ظ����)
    }

    // Update is called once per frame
    //void Update()
    //{
    //    CreatEnemyTime += Time.deltaTime;
    //    if( CreatEnemyTime > 3f) { Spawn(); }
    //}

    private void Spawn()
    {
        
        Instantiate(Enemy,CreateEnemyPoint.transform.position,CreateEnemyPoint.transform.rotation);
    }
}
