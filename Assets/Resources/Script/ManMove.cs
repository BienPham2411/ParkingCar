using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManMove : MonoBehaviour
{
    public float speed = 5f;
    private float distance;
    private Vector3 startPos;
    public Animator anim;
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
        anim.enabled = true;
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
        float time = 0;
        while(curDist < dist){
            if(isEnd())
                break;
            time += Time.deltaTime;
            if(isStay())
                setMove();
            yield return new WaitWhile(() => isStop());
            curDist = (transform.localPosition - startPos).magnitude;
            if(time >= 0.5f){
                anim.enabled = false;
                yield return new WaitForSeconds(0.5f);
                if(!isEnd())
                    anim.enabled = true;
                time = 0;
            }
            if(!isReverse){
                transform.localPosition += new Vector3(speed , 0, 0) * Time.deltaTime;
            }else{
                transform.localPosition += new Vector3(-speed , 0, 0) * Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }
        if(isMove())
            setStay();
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
        float time = 0;
        while(curDist < dist){
            if(isEnd())
                break;
            time += Time.deltaTime;
            if(isStay())
                setMove();
            yield return new WaitWhile(() => isStop());
            curDist = (transform.localPosition - startPos).magnitude;
            if(time >= 0.5f){
                anim.enabled = false;
                yield return new WaitForSeconds(0.5f);
                if(!isEnd())
                    anim.enabled = true;
                time = 0;
            }
            if(!isReverse){
                transform.localPosition += new Vector3(0 , 0, speed) * Time.deltaTime;
            }else{
                transform.localPosition += new Vector3(0 , 0, -speed) * Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }
        if(isMove())
            setStay();
    }

    IEnumerator RotateMe(float deg){
        float curDeg = 0;
        float time = 0.15f;
        float timeScale = time / Time.deltaTime;
        while(curDeg < Mathf.Abs(deg)){
            curDeg += Mathf.Abs(deg)/timeScale;
            transform.RotateAround(transform.position, Vector3.up, deg / timeScale);
            time -= timeScale;
            yield return new WaitForFixedUpdate();
        }
    }

    public void setStay(){
        Debug.Log("Stay");
        _moveState = MoveState.Stay;
        anim.enabled = false;
    }
    public bool isStay(){
        return _moveState == MoveState.Stay;
    }

    public bool isMove(){
        return _moveState == MoveState.Move;
    }

    public void setMove(){
        Debug.Log(("Move"));
        _moveState = MoveState.Move;
        anim.enabled = true;
    }

    public bool isEnd(){
        return _moveState == MoveState.End;
    }

    public void setEnd(){
        Debug.Log("End");
        _moveState = MoveState.End;
        anim.enabled = false;
    }

    public void setStop(){
        Debug.Log(("Stop"));
        _moveState = MoveState.Stop;
        anim.enabled = false;
    }

    public bool isStop(){
        return _moveState == MoveState.Stop;
    }
}
