using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControler : MonoBehaviour
{
    public bool moveX;
    public bool moveZ;
    public float speed;
    private Vector3 posM;
    private Camera cam;
    private float cameraZDist;
    private float turnSpeed = 90f;
    private Vector3 curPos;
    private Vector3 startPos;
    private Vector3 dist;
    private int dirX, dirZ;
    private Rigidbody rb;
    public bool isMove;
    private enum TouchStatus
    {
        None,
        Enter,
        Drag,
        Exit,
    }
    private TouchStatus  _touchStatus = TouchStatus.None;
    private bool isTurn;
    private bool priority;
    private void Awake() {
        cam = Camera.main;
        cameraZDist = cam.WorldToScreenPoint(transform.position).z;
        Init();
        priority = false;
        rb = transform.GetComponent<Rigidbody>();
    }

    private void OnMouseDown() {
        if(_touchStatus == TouchStatus.Drag)
            return;
        if(_touchStatus == TouchStatus.Enter)
            Init();
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDist);
        Vector3 newWorldPos = cam.ScreenToWorldPoint(screenPos);
        startPos = new Vector3(newWorldPos.x, transform.localPosition.y, newWorldPos.z);
    }

    private void OnMouseDrag() {
        if(_touchStatus == TouchStatus.Drag || _touchStatus == TouchStatus.Enter)
            return;
        Debug.Log("Drag");
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDist);
        Vector3 newWorldPos = cam.ScreenToWorldPoint(screenPos);
        curPos = new Vector3(newWorldPos.x, transform.localPosition.y, newWorldPos.z);
        dist = curPos - startPos;
        if(dist.magnitude > 1f){
            Debug.Log("Drag");

            _touchStatus = TouchStatus.Drag;
            if(dist.x < 0)
                dirX = -1;
            else    
                dirX = 1;
            if(dist.z < 0)
                dirZ = -1;
            else    
                dirZ = 1;
        }
    }
    private void Update() {
        if(!GameManager.instance.getIsPlay())
            return;
        if(_touchStatus == TouchStatus.Drag){
            if(moveX)
                transform.localPosition += new Vector3(speed * dirX, 0, 0) * Time.deltaTime;
            if(moveZ)
                transform.localPosition += new Vector3(0, 0, speed * dirZ) * Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Car" && _touchStatus == TouchStatus.Drag){
            if(!this.priority && !other.GetComponent<CarControler>().priority){
                Debug.Log("Car collide");
                _touchStatus = TouchStatus.Enter;
                StartCoroutine(moveBackward());
                // rb.velocity = Vector3.zero;
                // other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }else
            if(!this.priority && other.GetComponent<CarControler>().priority){
                Debug.Log("Car collide");
                _touchStatus = TouchStatus.Enter;
                StartCoroutine(moveBackward());
                WaitAndGo();
            }
        }

        if(other.tag == "Obstacle"){
            Debug.Log("Obstacle");
            _touchStatus = TouchStatus.Enter;
            StartCoroutine(moveBackward());
            // rb.velocity = Vector3.zero;
            // other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if(other.tag == "Grandma"){
            Debug.Log("Hit people");
            other.GetComponent<GrandmaMove>().setMove(false, false);
            _touchStatus = TouchStatus.Enter;
            GameManager.instance.setIsPlay(false);
        }
    }

    private void OnMouseUp() {
        if(_touchStatus != TouchStatus.Drag)
            Init();
    }
    public void WaitAndGo(){
        StartCoroutine(WaitForCar());
    }

    IEnumerator WaitForCar(){
        yield return new WaitForSeconds(0.7f);
        _touchStatus = TouchStatus.Drag;
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime) {    
        Quaternion fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for(var t = 0f; t < 1; t += Time.deltaTime/inTime) {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    IEnumerator moveBackward(){
        float moveTime = 0;
        float newSpeed = -speed / 8;
        while(moveTime < 0.25f){
            moveTime += Time.deltaTime;
            if(moveX)
                transform.localPosition += new Vector3(newSpeed * dirX, 0, 0) * Time.deltaTime;
            if(moveZ)
                transform.localPosition += new Vector3(0, 0, newSpeed * dirZ) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public void Init(){
        // curPos = transform.localPosition;
        _touchStatus = TouchStatus.None;

    }

    public void TurnAround(){
        if(isTurn)
            return;
        priority = true;
        float angle = transform.localEulerAngles.y;
        // Debug.Log(angle);
        if(checkAngle(angle)){
            transform.RotateAround(transform.position, Vector3.up, 90);  
            // StartCoroutine(RotateMe(Vector3.up * 90, 0.2f));
        }else{
            transform.RotateAround(transform.position, Vector3.up, -90);
            // StartCoroutine(RotateMe(Vector3.up * -90, 0.2f));
            
        }
        if(moveX)
            moveX = false;
        else    
            moveX = true;
        if(moveZ)
            moveZ = false;
        else    
            moveZ = true;
        SetDir();
        SetTurn(true);
    }

    public void SetTurn(bool _isTurn){
        isTurn = _isTurn;
    }

    public void SetDir(){
        if(dirX == 1 && dirZ == 1){
            dirZ = -1;
        }else
        if(dirX == 1 && dirZ == -1){
            dirX = -1;
        }else
        if(dirX == -1 && dirZ == -1){
            dirZ = 1;
        }else{
            dirX = 1;
        }
    }

    private bool checkAngle(float angle){
        if(dirX > 0 && dirZ > 0 && angle > -5 && angle < 5){
            return true;
        }
        if(dirX > 0 && dirZ > 0 && angle > 175 && angle < 185){
            return false;
        }
        if(dirX > 0 && dirZ < 0 && angle > 85 && angle < 95){
            return true;
        }
        if(dirX > 0 && dirZ < 0 && angle > 265 && angle < 275){
            return false;
        }
        if(dirX < 0 && dirZ < 0 && angle > 175 && angle < 185){
            return true;
        }
        if(dirX < 0 && dirZ < 0 && angle > -5 && angle < 5){
            return false;
        }
        if(dirX < 0 && dirZ > 0 && angle > 265 && angle < 275){
            return true;
        }
        if(dirX < 0 && dirZ > 0 && angle > 85 && angle < 95){
            return false;
        }
        Debug.Log("dirX: " + dirX + " dirZ: " + dirZ + " angle: " + angle);
        return false;   
    }
}   
