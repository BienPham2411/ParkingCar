using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    public List<GameObject> listLine;
    private GameObject cars;
    private int totalCars;
    private int countCars;
    private Vector2 curPOs;
    private GameObject curLevel;
    private int level;
    private bool isPlay;
    private void Awake() {
        instance = this;
        InitLevel();
    }

    public void InitLevel(){
        if(curLevel != null)
            Destroy(curLevel);
        isPlay = true;
        level = PlayerPrefs.GetInt("Level", 0);
        GameObject clone = Resources.Load<GameObject>("Levels/Level " + level);
        curLevel = Instantiate(clone, transform.position, clone.transform.rotation);
        cars = curLevel.transform.Find("Cars").gameObject;
        totalCars = cars.transform.childCount;
        countCars = 0;
    }

    public void CarOut(){
        countCars ++;
        CheckWin();
    }
    
    public void CheckWin(){
        if(countCars >= totalCars){
            WinGame();
        }
    }

    public void WinGame(){
        Debug.Log("Win");
    }

    public bool getIsPlay(){
        return isPlay;
    }

    public void setIsPlay(bool _isPlay){
        this.isPlay = _isPlay;
    }
}
