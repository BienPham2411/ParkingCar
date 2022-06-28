using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance;
    private int level = 0;
    private ManMove man;
    private GameObject curLevel;
    // Start is called before the first frame update

    private void Awake() {
        instance = this;
    }

    public void setUpLevel(){
        if(level == 1){
            StartCoroutine(level1());
        }
        if(level == 2){
            StartCoroutine(level2());
        }
    }

    public void setLevel(int _level){
        level = _level;
    }

    public void setCurLevel(GameObject _curLevel){
        curLevel = _curLevel;
    }

    IEnumerator level1(){
        man = curLevel.transform.Find("Human").GetChild(0).GetComponent<ManMove>();
        while(!man.isEnd()){
            man.moveX(3.73f, false);
            yield return new WaitUntil(() => man.isStay());
            man.moveZ(5f, false);
            yield return new WaitUntil(() => man.isStay());
            man.moveZ(5f, true);
            yield return new WaitUntil(() => man.isStay());
            man.moveX(3.73f, true);
            yield return new WaitUntil(() => man.isStay());
        }
    }

    IEnumerator level2(){
        man = curLevel.transform.Find("Human").GetChild(0).GetComponent<ManMove>();
        while(!man.isEnd()){
            man.moveX(1.7f, false);
            yield return new WaitUntil(() => man.isStay());
            man.moveZ(2.4f, true);
            yield return new WaitUntil(() => man.isStay());
            man.moveZ(2.4f, false);
            yield return new WaitUntil(() => man.isStay());
            man.moveX(1.73f, true);
            yield return new WaitUntil(() => man.isStay());
        }
    }
}
