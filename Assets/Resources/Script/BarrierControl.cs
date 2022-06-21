using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierControl : MonoBehaviour
{
    private Animator barrierAnim;
    private float timeOpen;
    private void Awake() {
        barrierAnim = GetComponent<Animator>();
        timeOpen = 0f;
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Car"){
            if(timeOpen == 0){
                StartCoroutine(openAndClose());
            }
            timeOpen = 0;
        }
    }

    IEnumerator openAndClose(){
        yield return new WaitWhile(() => barrierAnim.enabled);
        StartCoroutine(openBarri());
        while(timeOpen < 1f){
            timeOpen += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(closeBarri());
        timeOpen = 0;
    }

    IEnumerator closeBarri(){
        barrierAnim.enabled = true;
        barrierAnim.Play("barrierAnim", -1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        barrierAnim.enabled = false;      
    }
    IEnumerator openBarri(){
        barrierAnim.enabled = true;
        barrierAnim.Play("barrierAnim", -1, 0);
        yield return new WaitForSeconds(0.5f);
        barrierAnim.enabled = false;
    }
}
