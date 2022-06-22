using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaMove : MonoBehaviour
{
    public float speed = 5f;
    private float distance;
    private Vector3 startPos;
    private enum MoveState{
        Move,
        Stay,
        Stop,
        End
    }
    MoveState _moveState = MoveState.Stay;
    private void OnEnable() {
        startPos = transform.localPosition;    
        speed = 5f;
    }

    public void setSpeed(float _speed){
        speed = _speed;
    }

    public void moveX(float dist, bool isReverse){
        StartCoroutine(moveDirX(dist, isReverse));
    }

    public void moveZ(float dist, bool isReverse){
        StartCoroutine(moveDirZ(dist, isReverse));
    }

    IEnumerator moveDirX(float dist, bool isReverse){
        float startRot = transform.localEulerAngles.y;
        float angle;
        if(!isReverse){
            angle = 90f - startRot;
        }else{
            angle = -90f - startRot;
        }
        if(angle > 180)
            angle = 360 - angle;
        else if(angle < -180)
            angle += 360;
        StartCoroutine(RotateMe(angle));
        Debug.Log("Move X");
        Vector3 startPos = transform.localPosition;
        float curDist = 0;
        while(curDist < dist){
            if(_moveState == MoveState.Stay)
                _moveState = MoveState.Move;
            yield return new WaitWhile(() => isStop());
            curDist = (transform.localPosition - startPos).magnitude;
            if(!isReverse){
                transform.localPosition += new Vector3(speed , 0, 0) * Time.deltaTime;
            }else{
                transform.localPosition += new Vector3(-speed , 0, 0) * Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
            _moveState = MoveState.Stay;
        }
    }

    IEnumerator moveDirZ(float dist, bool isReverse){
        float startRot = transform.localEulerAngles.y;
        float angle;
        if(!isReverse){
            angle = 0f - startRot;
        }else{
            angle = 180f - startRot;
        }
        if(angle > 180)
            angle = 360 - angle;
        else if(angle < -180)
            angle += 360;
        StartCoroutine(RotateMe(angle));
        Debug.Log("Move Z");
        Vector3 startPos = transform.localPosition;
        float curDist = 0;
        if(_moveState == MoveState.Stay)
            _moveState = MoveState.Move;
        while(curDist < dist){
            curDist = (transform.localPosition - startPos).magnitude;
            if(_moveState == MoveState.Stay)
                _moveState = MoveState.Move;
            yield return new WaitWhile(() => isStop());
            if(!isReverse){
                transform.localPosition += new Vector3(0 , 0, speed) * Time.deltaTime;
            }else{
                transform.localPosition += new Vector3(0 , 0, -speed) * Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }
        _moveState = MoveState.Stay;
    }

    IEnumerator RotateMe(float deg){
        float curDeg = 0;
        float time = 0.2f;
        float timeScale = time / Time.deltaTime;
        while(curDeg < Mathf.Abs(deg)){
            curDeg += Mathf.Abs(deg)/timeScale;
            transform.RotateAround(transform.position, Vector3.up, deg / timeScale);
            time -= timeScale;
            yield return new WaitForFixedUpdate();
        }
    }

    public void setStay(){
        _moveState = MoveState.Stay;
    }
    public bool isStay(){
        return _moveState == MoveState.Stay;
    }

    public bool isMove(){
        return _moveState == MoveState.Move;
    }

    public void setMove(){
        _moveState = MoveState.Move;
    }

    public bool isEnd(){
        return _moveState == MoveState.End;
    }

    public void setEnd(){
        _moveState = MoveState.End;
    }

    public void setStop(){
        _moveState = MoveState.Stop;
    }

    public bool isStop(){
        return _moveState == MoveState.Stop;
    }
}
