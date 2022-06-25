using System.Collections;
using UnityEngine;

public class RoadControl : MonoBehaviour
{
    public bool isTurn = false;
    public bool isOut = false;
    public CarControler car;
    private void OnTriggerEnter(Collider other) {
        car = other.GetComponentInParent<CarControler>();
        if(other.tag == "Car Center"){
            // Debug.Log("Turn");
            car.addCarCoin();
            car.TurnAround();
            if(isTurn){
                Debug.Log("Turn again");
            car.SetTurn(false);
            car.TurnAround();
            }
            if(isOut){
                Debug.Log("Car Out");
                GameManager.instance.CarOut();
            }
        }
        
    }

    
    
}
