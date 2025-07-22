using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{   
    private CharacterController player;
    public float Speed=6f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player=GetComponent<CharacterController>();
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");//û�й���ֵ��ֻ��0��1
        float vertical = Input.GetAxisRaw("Vertical");
       //�ƶ�
        Move(horizontal, vertical);
        //��ת
        Truning();
        isRun(horizontal, vertical);
    }

    void Move(float horizontal,float vertical)
    {
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        dir = dir.normalized;//��һ������ֹб���ٶȱ�ֱ�߿�
        player.SimpleMove(dir * Speed);
    }
    void Truning()
    {   //�����λ����������
        Ray cameraRay=Camera.main.ScreenPointToRay(Input.mousePosition);
        //����Floorͼ�����
        int floorLayer = LayerMask.GetMask("Floor");
        RaycastHit hit;
        //���߼�⣨�������ƣ�out hit,���߳��ȣ����ͼ�㣩
        bool res=Physics.Raycast(cameraRay, out hit,100,floorLayer);
        if ((res==true))
        {   //ʹ��lookat
            //transform.LookAt(hit.point);


            //������ת������
            Vector3 v3 = hit.point - transform.position;
            v3.y = 0;
            //������תΪ��ת��
            Quaternion quaternion = Quaternion.LookRotation(v3);
            //���ʵ����ת
            GetComponent<Rigidbody>().MoveRotation(quaternion);
            //transform.rotation = quaternion;
        }
    }

    void isRun(float horizontal, float vertical)
    {    //�򵥷���
        //bool isW = horizontal != 0 || vertical != 0;
        //animator.SetBool("IsRun", isW);
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("IsRun", true);
        }
        
        else { animator.SetBool("IsRun", false); }
       
    }
    
}
