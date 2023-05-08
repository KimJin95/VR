using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCast : MonoBehaviour
{
    Transform myTr;

    Ray ray;
    RaycastHit hit;

    float dist = 10;

    LayerMask boxLayer;
    LayerMask buttonLayer;
    LayerMask layerMask;

    Animator anim;

    void Start()
    {
        myTr = transform;
        anim = GetComponentInChildren<Animator>();  

        boxLayer = 1 << LayerMask.NameToLayer("Box");
        buttonLayer = 1 << LayerMask.NameToLayer("Button");
        layerMask = boxLayer | buttonLayer;
    }


    void Update()
    {
        ray = new Ray(myTr.position, myTr.forward);

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);


        //x<<y(비트연산자 -> 왼쪽으로 y칸 이동)
        if (Physics.Raycast(ray, out hit, dist, layerMask))
        {
            PlayerMove.isStopped = true;
            anim.SetBool("isLook", true);
        }
        else
        {
            PlayerMove.isStopped = false;
        }

    }
}
