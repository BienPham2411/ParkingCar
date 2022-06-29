using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance;
    private int level = 0;
    public ManMove[] man;
    private GameObject curLevel;
    // Start is called before the first frame update

    private void Awake() {
        instance = this;
    }

    public void setUpLevel(){
        switch(level){
            case 1:
                StartCoroutine(level1());
                break;
            case 2:
                StartCoroutine(level2());
                break;
            case 4:
                StartCoroutine(level4());
                break;
            case 5:
                for (int i = 0; i <= 3; i++)
                {
                    StartCoroutine(level5(i));
                }
                break;
        }
    }

    public void setLevel(int _level){
        level = _level;
    }

    public void setCurLevel(GameObject _curLevel){
        curLevel = _curLevel;
    }

    IEnumerator level1(){
        man[0] = curLevel.transform.Find("Human").GetChild(0).GetComponent<ManMove>();
        while(!man[0].isEnd()){
            man[0].moveX(3.73f, false);
            yield return new WaitUntil(() => man[0].isStay());
            man[0].moveZ(5f, false);
            yield return new WaitUntil(() => man[0].isStay());
            man[0].moveZ(5f, true);
            yield return new WaitUntil(() => man[0].isStay());
            man[0].moveX(3.73f, true);
            yield return new WaitUntil(() => man[0].isStay());
        }
    }

    IEnumerator level2(){
        man[0] = curLevel.transform.Find("Human").GetChild(0).GetComponent<ManMove>();
        while(!man[0].isEnd()){
            man[0].moveX(1.7f, false);
            yield return new WaitUntil(() => man[0].isStay());
            man[0].moveZ(2.4f, true);
            yield return new WaitUntil(() => man[0].isStay());
            man[0].moveZ(2.4f, false);
            yield return new WaitUntil(() => man[0].isStay());
            man[0].moveX(1.73f, true);
            yield return new WaitUntil(() => man[0].isStay());
        }
    }

    IEnumerator level4(){
        man[0] = curLevel.transform.Find("Human").GetChild(0).GetComponent<ManMove>();
        while(!man[0].isEnd()){
            man[0].moveZ(3.14f, true);
            yield return new WaitUntil(() => man[0].isStay());
            man[0].moveX(0.8f, false);
            yield return new WaitUntil(() => man[0].isStay());
            man[0].moveZ(3.14f, false);
            yield return new WaitUntil(() => man[0].isStay());
            man[0].moveX(2.02f, false);
            yield return new WaitUntil(() => man[0].isStay());
            man[0].moveX(2.82f, true);
            yield return new WaitUntil(() => man[0].isStay());
        }
    }
    IEnumerator level5(int i){
        man[i] = curLevel.transform.Find("Human").GetChild(i).GetComponent<ManMove>();
        switch(i){
            case 0:
                while(!man[i].isEnd()){
                    man[i].moveZ(4.4f, false);
                    yield return new WaitUntil(() => man[i].isStay());
                    man[i].moveZ(4.4f, true);
                    yield return new WaitUntil(() => man[i].isStay());
                }
            break;
            case 1:
                while(!man[i].isEnd()){
                    man[i].moveX(4.4f, false);
                    yield return new WaitUntil(() => man[i].isStay());
                    man[i].moveX(4.4f, true);
                    yield return new WaitUntil(() => man[i].isStay());
                }
            break;
            case 2:
                while(!man[i].isEnd()){
                    man[i].moveZ(4.4f, true);
                    yield return new WaitUntil(() => man[i].isStay());
                    man[i].moveZ(4.4f, false);
                    yield return new WaitUntil(() => man[i].isStay());
                }
            break;
            case 3:
                while(!man[i].isEnd()){
                    man[i].moveX(4.4f, true);
                    yield return new WaitUntil(() => man[i].isStay());
                    man[i].moveX(4.4f, false);
                    yield return new WaitUntil(() => man[i].isStay());
                }
            break;
        }
    }
}
