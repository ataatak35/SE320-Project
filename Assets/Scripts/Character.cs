using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour{
    public float characterHealth = 100;
    public bool isDead;
    public TextMeshProUGUI  healthText;
    
    void Start(){
        isDead = false;
    }
    
    void Update(){
        healthText.text = characterHealth.ToString();
        if (characterHealth <= 0){
            Die();
        }
    }
    
    public void GetDamage(float damage){
        characterHealth -= damage;
        if (characterHealth <= 0){
            Die();
        }
    }

    public void Die(){
        isDead = true;
        GameManager.instance.EndGame();
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("1");
        if (other.gameObject.name == "FinishTrigger"){
            GameManager.instance.FinishGame();
        }
        
        
    }
}
