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
        enemyPS=gameObject.GetComponentInChildren<ParticleSystem>();//��������������ϵͳ���
        enemyAnimator=gameObject.GetComponent<Animator>();
        enemyCC=gameObject.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSiking)
        {   //ʬ���³�
            transform.Translate(Vector3.down*2.5f*Time.deltaTime);
        }
    }

    public void TakeDamage(int amount,Vector3 hitPoint)
    {
        if (IsDeath) { return; }//����Ѿ�������ֱ�ӷ���

        //������Ч����
        enemyAudioSource.PlayOneShot(enemyAudioSource.clip);
        //����������Ч����λ��
        //���û��λ�ã����������position
        enemyPS.transform.position = hitPoint;//������һ���λ��Ϊ��Ч����λ��
        enemyPS.Play();

        //�ܵ��˺���Ѫ
        StartingHealth -= amount;
        //�ж�Ѫ��
        if (StartingHealth <= 0)
        {   
            //������������
            Dealth();
        }

    }
    private void Dealth()
    {   
        IsDeath = true;
        //ʹ���岻������ϵͳӰ�죬������������
        GetComponent<Rigidbody>().isKinematic = true;

        enemyCC.isTrigger=true;

        //������������
        enemyAnimator.SetTrigger("IsDeath");//�����������л�����
        //����������Ч
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
