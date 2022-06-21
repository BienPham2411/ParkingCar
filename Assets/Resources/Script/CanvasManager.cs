using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    public Button btnReplay;
    private void Awake() {
        btnReplay.onClick.AddListener(ReplayLevel);
    }

    public void ReplayLevel(){
        GameManager.instance.InitLevel();
    }
}
