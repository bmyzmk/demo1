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

    //开枪相关变量
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
        //获取开火键
        time += Time.deltaTime;//设置时间间隔，否则开枪播放音效导致卡死
        if (Input.GetMouseButton(0)&&time>=timeBetweenBullets) { Shoot(); }
        //时间超过，火光特效消失
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
        //物体是否启用用gameobject.setactive(),游戏组件用  .enabled

        gunLine.SetPosition(0, transform.position);//参数（点序号，点位置(vector3)） 起点
        gunLine.SetPosition(1,transform.position+transform.forward*100);//终点
        //transform.forward是物体坐标系的前方，vector3.forward是大坐标系的前方
        gunLine.enabled=true;

        //播放粒子组件
        gunParticle.Play();

        //射击，检测是否击中敌人
        //定义射线
        shootRay.origin = transform.position;//起点
        shootRay.direction = transform.forward;//方向

        if (Physics.Raycast(shootRay, out hit, 100, shootMask))
        {
            gunLine.SetPosition(1, hit.point);//若击中敌人，则射线终点改为击中位置
            MyEnemyHealth enemyHealth=hit.collider.gameObject.GetComponent<MyEnemyHealth>();
            enemyHealth.TakeDamage(20,hit.point);//返回击中位置给血量系统，在击中位置产生击中特效

        }
        else
        {
            gunLine.SetPosition(1, transform.position + transform.forward * 100);

        }
    }
}
