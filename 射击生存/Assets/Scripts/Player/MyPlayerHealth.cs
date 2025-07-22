using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyPlayerHealth : MonoBehaviour
{
    public int PlayerStratingHealth = 100;
    public bool IsPlayerDeath=false;
    public AudioClip PlayerDeathClip;
    //���UI�ؼ�
    public Text PlayerHealthUI;
    //��������ڵ���
    public Image DamageImage;
    public Color FlashColor=new Color(1f,0f,0f,0.1f);

    private AudioSource PlayerAC;
    private Animator PlayerAni;
    private playercontrol playercontrol;
    private GunShoot GunShoot;
    private bool damaged=false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAC = GetComponent<AudioSource>();
        PlayerAni = GetComponent<Animator>();
        playercontrol=GetComponent<playercontrol>();
        GunShoot=GetComponentInChildren<GunShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            DamageImage.color = FlashColor;
        }
        else { DamageImage.color = Color.Lerp(DamageImage.color,Color.clear,5f*Time.deltaTime); }
        damaged = false;
    }
    public void TakenDamage(int amount)
    {
        if (IsPlayerDeath) { return; }

        damaged = true;

        PlayerStratingHealth -= amount;
        
        //�������Ѫ��UI Text
        PlayerHealthUI.text = PlayerStratingHealth.ToString();

        Debug.Log(PlayerStratingHealth);

        //������������
        PlayerAC.Play();

        if (PlayerStratingHealth<=0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        IsPlayerDeath = true;
        PlayerAC.clip= PlayerDeathClip;
        PlayerAC.Play();

        PlayerAni.SetTrigger("IsDeath");

        //�����ƶ����������
        playercontrol.enabled = false;
        GunShoot.enabled = false;
    }

   public void RestartLevel()
    {   
        //���¼��س��������¿�ʼ
        SceneManager.LoadScene(0);
    }
}
