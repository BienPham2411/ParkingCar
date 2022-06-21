using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaMove : MonoBehaviour
{
    public float speed = 1f;
    public bool moveX;
    public bool moveZ;
    public float period;
    private float distance;
    private Vector3 startPos;

    private void Awake() {
        startPos = transform.localPosition;    
    }

    void Update()
    {
        if(moveX){
            transform.localPosition += new Vector3(speed , 0, 0) * Time.deltaTime;
        }
        if(moveZ){
            transform.localPosition += new Vector3(0, 0, speed) * Time.deltaTime;
        }
        Vector3 distVec = transform.localPosition - startPos;
        distance = distVec.magnitude;
        if(distance >= period || distance <= 0.1f){
            speed *= -1;
        }
    }

    public void setMove(bool _moveX, bool _moveZ){
        this.moveX = _moveX;
        this.moveZ = _moveZ;
    }
}
