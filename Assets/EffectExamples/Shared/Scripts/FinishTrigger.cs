using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        Debug.Log("1");
        if (other.gameObject.CompareTag("Player")){
            GameManager.instance.FinishGame();
        }
    }
}
