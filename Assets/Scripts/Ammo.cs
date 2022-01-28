using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour{
    public GameObject pistol;
    private Gun gun;
    
    // Start is called before the first frame update
    void Start(){
        gun = GameObject.Find("pistol").GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other){
        if (other.transform.name == "Player"){
            Debug.Log("Ok");
            gun.GetAmmo();
            Destroy(gameObject);
        }
    }
}
