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
    //玩家UI控件
    public Text PlayerHealthUI;
    //玩家受伤遮挡层
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
        
        //更新玩家血量UI Text
        PlayerHealthUI.text = PlayerStratingHealth.ToString();

        Debug.Log(PlayerStratingHealth);

        //播放受伤声音
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

        //禁用移动，禁用射击
        playercontrol.enabled = false;
        GunShoot.enabled = false;
    }

   public void RestartLevel()
    {   
        //重新加载场景，重新开始
        SceneManager.LoadScene(0);
    }
}
