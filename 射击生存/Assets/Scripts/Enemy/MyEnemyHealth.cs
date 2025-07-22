using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyHealth : MonoBehaviour
{
    public AudioClip DealthClip;
    public int StartingHealth=100;
    public bool IsDeath=false;
    private bool IsSiking=false;

    private AudioSource enemyAudioSource;
    private ParticleSystem enemyPS;
    private Animator enemyAnimator;
    private CapsuleCollider enemyCC;
    // Start is called before the first frame update
    void Start()
    {
        enemyAudioSource=gameObject.GetComponent<AudioSource>();
        enemyPS=gameObject.GetComponentInChildren<ParticleSystem>();//调用子物体粒子系统组件
        enemyAnimator=gameObject.GetComponent<Animator>();
        enemyCC=gameObject.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSiking)
        {   //尸体下沉
            transform.Translate(Vector3.down*2.5f*Time.deltaTime);
        }
    }

    public void TakeDamage(int amount,Vector3 hitPoint)
    {
        if (IsDeath) { return; }//如果已经死亡，直接返回

        //受伤音效播放
        enemyAudioSource.PlayOneShot(enemyAudioSource.clip);
        //调整受伤特效播放位置
        //组件没有位置，调用物体的position
        enemyPS.transform.position = hitPoint;//设置玩家击中位置为特效产生位置
        enemyPS.Play();

        //受到伤害扣血
        StartingHealth -= amount;
        //判断血量
        if (StartingHealth <= 0)
        {   
            //调用死亡操作
            Dealth();
        }

    }
    private void Dealth()
    {   
        IsDeath = true;
        //使物体不受物理系统影响，减少性能消耗
        GetComponent<Rigidbody>().isKinematic = true;

        enemyCC.isTrigger=true;

        //播放死亡动画
        enemyAnimator.SetTrigger("IsDeath");//触发器设置切换动画
        //播放死亡音效
        enemyAudioSource.clip = DealthClip;
        enemyAudioSource.Play();

        MyPlayerScore.Score++;

    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        IsSiking = true;
        Destroy(gameObject, 2f);
    }
}
