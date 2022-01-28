using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour{
    public float damage = 10f;
    public float range = 100f;
    public Camera camera;
    public ParticleSystem particleSystem;
    public GameObject impactEffect1;
    public GameObject impactEffect2;
    private int ammo = 40;
    public bool isAmmo;
    public TextMeshProUGUI ammoText;
    void Start(){
        isAmmo = false;
    }
    
    void Update(){
        
        
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }

        if (isAmmo){
            GetAmmo();
        }
        ammoText.text = ammo.ToString();
    }

    public void Shoot(){
        if (ammo > 0){
            particleSystem.Play();
            ammo--;
            RaycastHit rayHit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out rayHit, range)){
                Enemy enemy = new Enemy();
                if (rayHit.transform.name == "Root"){
                    enemy = rayHit.transform.parent.GetComponent<Enemy>();
                    GameObject impact2 = Instantiate(impactEffect2, rayHit.point, Quaternion.LookRotation(rayHit.normal));
                    Destroy(impact2, 2f);
                }
                else{
                    GameObject impact1 = Instantiate(impactEffect1, rayHit.point, Quaternion.LookRotation(rayHit.normal));
                    Destroy(impact1, 2f);
                }
                if (enemy != null){
                    enemy.GetDamage(damage);
                }
                
            }
        }
    }

    public void GetAmmo(){
        ammo += 10;
        isAmmo = false;
        Debug.Log("GetAmmo");
    }

    
}
