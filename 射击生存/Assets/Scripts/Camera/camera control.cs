using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;
    public float Smoothing = 5f;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        offset= transform.position-player.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, offset + player.transform.position, Smoothing * Time.deltaTime);

    }
    private void FixedUpdate()
    {
        //插值计算使相机平滑移动
        //transform.position = Vector3.Lerp(transform.position, offset + player.transform.position, Smoothing * Time.deltaTime);
    }
}
