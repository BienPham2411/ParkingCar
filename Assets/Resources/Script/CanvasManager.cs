using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    public Button btnReplay, btnNext;
    public GameObject txtCoin;
    public GameObject dialogWin;
    private void Awake() {
        instance = this;
        btnReplay.onClick.AddListener(ReplayLevel);
        btnNext.onClick.AddListener(NextLevel);
        SetTxtCoin(DataGame.instance.GetCurrenCoin());
    }

    public void ReplayLevel(){
        GameManager.instance.InitLevel();
    }

    public void SetTxtCoin(int _coin){
        txtCoin.GetComponent<Text>().text = _coin.ToString();
    }

    public void NextLevel(){
        GameManager.instance.InitLevel();
        OpenWinGame(false);

    }

    public void OpenWinGame(bool _isOpen){
        dialogWin.SetActive(_isOpen);
        btnReplay.interactable = !_isOpen;
    }
}
