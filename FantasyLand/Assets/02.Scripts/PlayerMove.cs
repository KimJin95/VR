using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public enum MoveType { WayPoint, Lookat }
    [SerializeField] MoveType moveType = MoveType.WayPoint;

    [SerializeField] float speed = 3;
    [SerializeField] float damping = 3;

    int nextIdx = 0;

    Transform camTr;
    Transform myTr;
    CharacterController myController;

    [SerializeField] Transform[] points;
    [SerializeField] List<Transform> pointList = new List<Transform>();

    void Start()
    {
        myController = GetComponent<CharacterController>();
        myTr = transform;
        camTr = Camera.main.transform;

        GameObject group = GameObject.Find("PointGroup");
        points = group.GetComponentsInChildren<Transform>();

        group.GetComponentsInChildren<Transform>(pointList);
        pointList.RemoveAt(0);
    }


    void Update()
    {
        switch (moveType)
        {
            case MoveType.WayPoint: MoveWayPoint(); break;
            case MoveType.Lookat: MoveLookAt(); break;
        }

    }
    private void MoveWayPoint()
    {
        Vector3 dir = pointList[nextIdx].position - myTr.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        myTr.rotation = Quaternion.Slerp(myTr.rotation, rot, damping * Time.deltaTime);
        myTr.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            //맨 마지막 포인트에 도달했을 때 처음 인덱스(0)로 변경
           
            nextIdx = (++nextIdx >= pointList.Count) ? 0 : nextIdx;
        }
    }
    private void MoveLookAt()
    {
        Vector3 dir = camTr.forward;
        dir.y = 0;

        Debug.DrawRay(myTr.position, dir.normalized,Color.red);  

        myController.SimpleMove(dir*speed);
    }




}
