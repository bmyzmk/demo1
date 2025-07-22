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
        float horizontal = Input.GetAxisRaw("Horizontal");//没有过度值，只有0，1
        float vertical = Input.GetAxisRaw("Vertical");
       //移动
        Move(horizontal, vertical);
        //旋转
        Truning();
        isRun(horizontal, vertical);
    }

    void Move(float horizontal,float vertical)
    {
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        dir = dir.normalized;//归一化，防止斜线速度比直线快
        player.SimpleMove(dir * Speed);
    }
    void Truning()
    {   //在鼠标位置生成射线
        Ray cameraRay=Camera.main.ScreenPointToRay(Input.mousePosition);
        //返回Floor图层序号
        int floorLayer = LayerMask.GetMask("Floor");
        RaycastHit hit;
        //射线检测（射线名称，out hit,射线长度，检测图层）
        bool res=Physics.Raycast(cameraRay, out hit,100,floorLayer);
        if ((res==true))
        {   //使用lookat
            //transform.LookAt(hit.point);


            //计算旋转的向量
            Vector3 v3 = hit.point - transform.position;
            v3.y = 0;
            //将向量转为旋转角
            Quaternion quaternion = Quaternion.LookRotation(v3);
            //最后实现旋转
            GetComponent<Rigidbody>().MoveRotation(quaternion);
            //transform.rotation = quaternion;
        }
    }

    void isRun(float horizontal, float vertical)
    {    //简单方法
        //bool isW = horizontal != 0 || vertical != 0;
        //animator.SetBool("IsRun", isW);
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("IsRun", true);
        }
        
        else { animator.SetBool("IsRun", false); }
       
    }
    
}
