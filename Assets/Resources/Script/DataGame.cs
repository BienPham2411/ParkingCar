using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGame : MonoBehaviour
{
    public static DataGame instance;

    private void Awake() {
        instance = this;
    }

    public void SetCurrentCoin(int coins){
        PlayerPrefs.SetInt("COIN", coins);
    }

    public int GetCurrenCoin(){
        return PlayerPrefs.GetInt("COIN", 0);
    }

    public void SetCurrentLevel(int level){
        PlayerPrefs.SetInt("LEVEL", level);
    }

    public int GetCurrentLevel(){
        return PlayerPrefs.GetInt("LEVEL", 0);
    }

}
