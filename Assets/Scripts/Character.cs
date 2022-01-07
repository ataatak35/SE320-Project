using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour{
    public float characterHealth = 100;
    public bool isDead;
    public GameObject endGameCanvas;
    public TextMeshProUGUI  healthText;
    void Start(){
        isDead = false;
    }

    
    void Update(){
        healthText.text = characterHealth.ToString();
    }

    
    
    public void TakeDamage(float damage){
        characterHealth -= damage;
        if (characterHealth <= 0){
            Die();
        }
    }

    public void Die(){
        isDead = true;
        Time.timeScale = 0;
        endGameCanvas.SetActive(true);
    }
}
