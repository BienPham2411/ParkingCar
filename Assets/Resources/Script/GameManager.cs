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
    private int maxLevel = 2;
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
        level = DataGame.instance.GetCurrentLevel();
        if(level >= maxLevel)
            level = Random.Range(0, maxLevel);
        GameObject clone = Resources.Load<GameObject>("Levels/Level " + level);
        curLevel = Instantiate(clone, transform.position, clone.transform.rotation);
        LevelControl.instance.setLevel(level);
        LevelControl.instance.setCurLevel(curLevel);
        LevelControl.instance.setUpLevel();
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
        setLevel(level + 1);
        DataGame.instance.SetCurrentLevel(level);
    }

    public bool getIsPlay(){
        return isPlay;
    }

    public void setIsPlay(bool _isPlay){
        this.isPlay = _isPlay;
    }

    public int getLevel(){
        return level;
    }

    private void setLevel(int _level){
        this.level = _level;
    }
}
