using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPlayerScore : MonoBehaviour
{
    //score
    public static int Score = 0;//��̬ȫ�ֿɵ���
    //text
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = $"Score:{Score}";
    }
}
