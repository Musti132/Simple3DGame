using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerCoinCollider : MonoBehaviour
{
    private int userPoints;
    public TextMeshProUGUI userPointsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(userPoints);
        userPointsText.text = "Current points: " + userPoints;
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Coin") {
            userPoints += 1;
        }
    }
}
