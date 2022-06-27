using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance;
    private int level = 0;
    private GrandmaMove grandma;
    private GameObject curLevel;
    // Start is called before the first frame update

    private void Awake() {
        instance = this;
    }

    public void setUpLevel(){
        if(level == 1){
            StartCoroutine(level1());
        }
    }

    public void setLevel(int _level){
        level = _level;
    }

    public void setCurLevel(GameObject _curLevel){
        curLevel = _curLevel;
    }

    IEnumerator level1(){
        grandma = curLevel.transform.Find("Human").GetChild(0).GetComponent<GrandmaMove>();
        while(!grandma.isEnd()){
            grandma.moveX(3.73f, false);
            yield return new WaitUntil(() => grandma.isStay());
            grandma.moveZ(5f, false);
            yield return new WaitUntil(() => grandma.isStay());
            grandma.moveZ(5f, true);
            yield return new WaitUntil(() => grandma.isStay());
            grandma.moveX(3.73f, true);
            yield return new WaitUntil(() => grandma.isStay());
        }
    }
}
