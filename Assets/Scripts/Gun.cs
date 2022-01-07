using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem particleSystem;
    public GameObject impactEffect1;
    public GameObject impactEffect2;
    void Start(){
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    public void Shoot(){    
        particleSystem.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);

            Enemy enemy = new Enemy();
            if (hit.transform.name == "Root"){
                 enemy = hit.transform.parent.GetComponent<Enemy>();
                 GameObject impact2 = Instantiate(impactEffect2, hit.point, Quaternion.LookRotation(hit.normal));
                 Destroy(impact2, 2f);
            }
            else{
                 enemy = hit.transform.GetComponent<Enemy>();
            }
            
            if (enemy != null){
                enemy.TakeDamage(damage);
            }

            GameObject impact1 = Instantiate(impactEffect1, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact1, 2f);
        }
    }
}
