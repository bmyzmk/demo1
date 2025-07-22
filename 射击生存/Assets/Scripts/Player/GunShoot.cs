using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunShoot : MonoBehaviour
{   private AudioSource audioSource;
    private float time=0;
    private float timeBetweenBullets=0.15f;
    private float effectsDisPlayTime=0.2f;
    private Light gunLight;
    private LineRenderer gunLine;
    private ParticleSystem gunParticle;

    //��ǹ��ر���
    private Ray shootRay;
    private RaycastHit hit;
    private int shootMask;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
        gunParticle = GetComponent<ParticleSystem>();
        shootMask = LayerMask.GetMask("Shootable");

}

// Update is called once per frame
void Update()
    {
        //��ȡ�����
        time += Time.deltaTime;//����ʱ����������ǹ������Ч���¿���
        if (Input.GetMouseButton(0)&&time>=timeBetweenBullets) { Shoot(); }
        //ʱ�䳬���������Ч��ʧ
        if(time>=timeBetweenBullets*effectsDisPlayTime) 
        {
            gunLight.enabled = false;
            gunLine.enabled = false;
        }
    }

    void Shoot()
    {
        time = 0;
        audioSource.PlayOneShot(audioSource.clip);

        gunLight.enabled = true;
        //�����Ƿ�������gameobject.setactive(),��Ϸ�����  .enabled

        gunLine.SetPosition(0, transform.position);//����������ţ���λ��(vector3)�� ���
        gunLine.SetPosition(1,transform.position+transform.forward*100);//�յ�
        //transform.forward����������ϵ��ǰ����vector3.forward�Ǵ�����ϵ��ǰ��
        gunLine.enabled=true;

        //�����������
        gunParticle.Play();

        //���������Ƿ���е���
        //��������
        shootRay.origin = transform.position;//���
        shootRay.direction = transform.forward;//����

        if (Physics.Raycast(shootRay, out hit, 100, shootMask))
        {
            gunLine.SetPosition(1, hit.point);//�����е��ˣ��������յ��Ϊ����λ��
            MyEnemyHealth enemyHealth=hit.collider.gameObject.GetComponent<MyEnemyHealth>();
            enemyHealth.TakeDamage(20,hit.point);//���ػ���λ�ø�Ѫ��ϵͳ���ڻ���λ�ò���������Ч

        }
        else
        {
            gunLine.SetPosition(1, transform.position + transform.forward * 100);

        }
    }
}
