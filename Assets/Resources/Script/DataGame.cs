using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGame : MonoBehaviour
{
    public static DataGame instance;

    private void Awake() {
        instance = this;
        PlayerPrefs.SetInt("CAR_0", 1);
        PlayerPrefs.SetInt("TRAIL_0", 1);
        PlayerPrefs.SetInt("MAN_0", 1);
        PlayerPrefs.SetInt("ENVI_0", 1);
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

    public void SetDataCarBuy(int id){
        PlayerPrefs.SetInt("CAR_" + id, 1);
    }
    public void SetDataTrailBuy(int id){
        PlayerPrefs.SetInt("TRAIL_" + id, 1);
    }
    public void SetDataManBuy(int id){
        PlayerPrefs.SetInt("MAN_" + id, 1);
    }

    public void SetDataEnviBuy(int id){
        PlayerPrefs.SetInt("ENVI_" + id, 1);
    }

    public bool GetDataCareHave(int id){
        return PlayerPrefs.GetInt("CAR_" + id, 0) == 1;
    }
    public bool GetDataTrailBallHave(int id){
        return PlayerPrefs.GetInt("BALL_" + id, 0) == 1;
    }
    public bool GetDataManlHave(int id){
        return PlayerPrefs.GetInt("TRAIL_" + id, 0) == 1;
    }
    public bool GetDataEnviHave(int id){
        return PlayerPrefs.GetInt("ENVI_" + id, 0) == 1;
    }

    public void SetCurrentCar(int id){
        PlayerPrefs.SetInt("CAR", id);
    }

    public void SetCurrentTrail(int id){
        PlayerPrefs.SetInt("TRAIL", id);
    }

    public void SetCurrentMan(int id){
        PlayerPrefs.SetInt("MAN", id);
    }

    public void SetCurrentEnvi(int id){
        PlayerPrefs.SetInt("ENVI", id);
    }
}
