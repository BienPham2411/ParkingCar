using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerate : MonoBehaviour
{
    public static GridGenerate instance;
    public int x, z;
    public GameObject groundUnit;
    // Start is called before the first frame update

    private void Awake() {
        instance = this;
    }
    
    public void GanerateGrid(){
        for(int i = -x; i <= x; i ++){
            for(int j = -z; j <= z; j++){
                GameObject clone = Instantiate(groundUnit, new Vector3(i, 1, j), Quaternion.identity, transform);
                clone.name = "Tile " + x + " " + z;
                
            }
        }
    }
}
