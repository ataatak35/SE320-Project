using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour{
    public GameObject gate;
    private bool isOpen;
    private Quaternion firstRot;
    private Quaternion lastRot;
    public TextMeshProUGUI zombieAmountText;
    void Start(){
        isOpen = true;
        firstRot = gate.transform.rotation;
    }

    
    void Update()
    {
        if (GameManager.instance.zombieAmount == 0){
            Invoke("OpenTheGate", 1);
        }
    }

    public void CloseTheGate(){
        gate.transform.rotation = Quaternion.Lerp(firstRot, Quaternion.Euler(0,31,0), 0.5f);
        lastRot = gate.transform.rotation;
        isOpen = false;
        GameManager.instance.isCreating = true;
        zombieAmountText.transform.parent.gameObject.SetActive(true);
        Debug.Log(GameManager.instance.isCreating);
    }

    public void OpenTheGate(){
        gate.transform.rotation = Quaternion.Lerp(lastRot, firstRot, 0.5f);
        isOpen = true;
        gameObject.SetActive(false);
        GameManager.instance.ActiveFinishTrigger();
    }

    private void OnTriggerEnter(Collider other){
        if (other.transform.CompareTag("Player") && isOpen){
            CloseTheGate();
        }
    }
}
