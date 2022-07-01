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
    private Vector3 curPos;
    private Vector3 startPos;
    private Vector3 dist;
    private int dirX, dirZ;
    private Rigidbody rb;
    public bool isMove;
    private bool isAddCoin;
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
        InitCar();
        priority = false;
        isAddCoin = false;
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
        // Debug.Log("Drag");
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDist);
        Vector3 newWorldPos = cam.ScreenToWorldPoint(screenPos);
        curPos = new Vector3(newWorldPos.x, transform.localPosition.y, newWorldPos.z);
        dist = curPos - startPos;
        if(dist.magnitude > 2f){
            // Debug.Log("Drag");

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
                // Debug.Log("Car collide");
                _touchStatus = TouchStatus.Enter;
                StartCoroutine(moveBackward());
                if(!Input.GetMouseButton(0)){
                    Init();
                }
                // rb.velocity = Vector3.zero;
                // other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }else
            if(!this.priority && other.GetComponent<CarControler>().priority){
                // Debug.Log("Car collide");
                _touchStatus = TouchStatus.Enter;
                StartCoroutine(moveBackward());
                WaitAndGo();
            }
        }

        if(other.tag == "Obstacle" && !isTurn){
            // Debug.Log("Obstacle");
            _touchStatus = TouchStatus.Enter;
            StartCoroutine(moveBackward());
            if(!Input.GetMouseButton(0)){
                Init();
            }
            // rb.velocity = Vector3.zero;
            // other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if(other.tag == "Man" && _touchStatus == TouchStatus.Drag && !isTurn){
            Debug.Log("Hit people");
            _touchStatus = TouchStatus.Enter;
            GameManager.instance.setIsPlay(false);
            other.transform.GetComponent<ManMove>().setEnd();
            GameManager.instance.SetCarsCollide();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddExplosionForce(300f, other.transform.position + new Vector3(0, 0f, 0), 2f, 50f);
        }else
        if(other.tag == "Man" && _touchStatus != TouchStatus.Enter){
            Debug.Log("Collide again");
            ManMove man = other.transform.GetComponent<ManMove>();
            man.setStop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Man"){
            ManMove man = other.transform.GetComponent<ManMove>();
            if(man.isStop())
                man.setMove();
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


    IEnumerator moveBackward(){
        float moveTime = 0;
        float newSpeed = -speed / 8;
        while(moveTime < 0.2f){
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
            // transform.RotateAround(transform.position, Vector3.up, 90);  
            // StartCoroutine(RotateMe(Vector3.up * 90, 0.2f));
            StartCoroutine(RotateCar(90f));
        }else{
            // transform.RotateAround(transform.position, Vector3.up, -90);
            // StartCoroutine(RotateMe(Vector3.up * -90, 0.2f));
            StartCoroutine(RotateCar(-90f));
            
        }
    }

    public IEnumerator RotateCar(float deg){
        float curDeg = 0;
        float time = 0.2f;
        float timeScale = time / Time.deltaTime;
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
        while(curDeg < Mathf.Abs(deg)){
            curDeg += Mathf.Abs(deg)/timeScale;
            transform.RotateAround(transform.position, Vector3.up, deg / timeScale);
            time -= timeScale;
            yield return new WaitForFixedUpdate();
        }
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
        // Debug.Log("dirX: " + dirX + " dirZ: " + dirZ + " angle: " + angle);
        return false;   
    }

    public void addCarCoin(){
        if(!isAddCoin)    
            GameManager.instance.addCoin(Random.Range(2, 4));
        isAddCoin = true;
    }

    public int sizeCar(){
        if(GetComponent<BoxCollider>().size == new Vector3(1, 1, 1.7f))
            return 0;
        if(GetComponent<BoxCollider>().size == new Vector3(1, 1, 2.2f))
            return 1;
        return 2;
    }

    public void InitCar(){
        int size = sizeCar();
        Transform curBody = transform.Find("Body");
        if( curBody != null){
            Destroy(curBody.gameObject);
        }
        GameObject body;
        GameObject bodyOri;
        switch(size){
            case 0:
                bodyOri = Resources.Load<GameObject>("Vehicles/Short/" + Random.Range(0, 7) + "/Car 0").transform.Find("Body").gameObject;
                body = Instantiate(bodyOri, transform.position, transform.rotation, transform);
                break;
            case 1:
                bodyOri = Resources.Load<GameObject>("Vehicles/Medium/" + Random.Range(0, 7) + "/Car 0").transform.Find("Body").gameObject;
                body = Instantiate(bodyOri, transform.position, transform.rotation, transform);
                break;
            case 2:
                bodyOri = Resources.Load<GameObject>("Vehicles/Long/" + Random.Range(0, 7) + "/Car 0").transform.Find("Body").gameObject;
                body = Instantiate(bodyOri, transform.position, transform.rotation, transform);
                break;
            default:
                body = null;
                break;
        }
    }
}   

