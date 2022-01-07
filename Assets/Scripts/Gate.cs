using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour{
    public GameObject gate;
    private bool isOpen;
    void Start(){
        isOpen = true;
    }

    
    void Update()
    {
        
    }

    public void CloseTheGate(){
        gate.transform.rotation = Quaternion.Lerp(gate.transform.rotation, Quaternion.Euler(0,31,0), 0.5f);
        isOpen = false;
    }

    private void OnTriggerEnter(Collider other){
        if (other.transform.CompareTag("Player") && isOpen){
            CloseTheGate();
        }
    }
}
