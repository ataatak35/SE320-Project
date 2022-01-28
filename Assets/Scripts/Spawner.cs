using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{
    public GameObject enemy;
    public int createdEnemyAmount;
    public bool isCreating;
    
    void Start(){
        createdEnemyAmount = 0;
    }
    
    
    void Update()
    {

            if (GameManager.instance.isCreating){
                if (createdEnemyAmount > 9){
                    DestroySpawners();
                }
                else{
                    StartCoroutine("SpawnCoroutine");
                    Debug.Log("Spawn");
                }
                Debug.Log(createdEnemyAmount); 
            }
       
        
    }

    public void Spawn(){
        Instantiate(enemy, new Vector3(transform.position.x + Random.Range(0,5), transform.position.y, transform.position.z + Random.Range(0,5)), Quaternion.identity);
        createdEnemyAmount++;
    }

    public IEnumerator SpawnCoroutine(){
        GameManager.instance.isCreating = false;
        yield return new WaitForSeconds(3f);
        Spawn();
        GameManager.instance.isCreating = true;
    }
    
    public void DestroySpawners(){
        GameObject[] spawners ;
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach(GameObject spawner in spawners) {
            Destroy(spawner);
        }
    }
    
    
    
    
}
