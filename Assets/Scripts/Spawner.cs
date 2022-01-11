using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{
    public GameObject enemy;
    public int enemyAmount;
    public int maxEnemyAmount = 10;
    public bool isCreating;
    
    void Start(){
        enemyAmount = 0;
        InvokeRepeating("Spawn", 10, 2);
    }
    
    
    void Update()
    {
        if (enemyAmount == maxEnemyAmount){
            CancelInvoke();
        }
    }

    public void Spawn(){
        Instantiate(enemy, new Vector3(transform.position.x + Random.Range(0,5), transform.position.y, transform.position.z + Random.Range(0,5)), Quaternion.identity);
        enemyAmount++;
    }
    
    
}
