using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    public GameObject endGameCanvas, finishGameCanvas, pauseCanvas;
    public TextMeshProUGUI zombieAmountText;
    public static GameManager instance;
    private PlayerController playerController;
    public GameObject gun;
    private Gun pistol;
    public int zombieAmount;
    public GameObject finishTrigger;
    public bool isCreating;

    public GameObject player;
    // Start is called before the first frame update

    public void InitializeSingleton(){
        if (instance == null){
            instance = this;
            
        }
        else{
            Destroy(this);
        }
    }
    void Start(){
        isCreating = false;
        zombieAmount = 30;
        zombieAmountText.text = zombieAmount.ToString();
        DeactiveFinishTrigger();
        InitializeSingleton();
        playerController = player.GetComponent<PlayerController>();
        pistol = gun.GetComponent<Gun>();
    }
    
    void Update(){
        zombieAmountText.text = zombieAmount.ToString();
        if (Input.GetKeyDown(KeyCode.Escape)){
            Pause();
        }
    }

    public void Restart(){
        Debug.Log("Restart");
        SceneManager.LoadScene("Game");
    }

    public void EndGame(){
        DestroyZombies();
        endGameCanvas.SetActive(true);
        playerController.enabled = false;
        pistol.enabled = false;
        
    }

    public void BackToMainMenu(){
        Time.timeScale = 1;
        Debug.Log("Main Menu");
        SceneManager.LoadScene("MainScreen");
    }

    public void Pause(){
        Time.timeScale = 0;
        playerController.enabled = false;
        pistol.enabled = false;
        pauseCanvas.SetActive(true);
    }

    public void Resume(){
        Time.timeScale = 1;
        playerController.enabled = true;
        pistol.enabled = true;
        pauseCanvas.SetActive(false);
    }

    public void DestroyZombies(){
        GameObject[] zombies ;
        zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach(GameObject zombie in zombies) {
            Destroy(zombie);
        }
    }
    
    public void AttackZombies(){
        GameObject[] zombies ;
        zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach(GameObject zombie in zombies){
            zombie.GetComponent<NavMeshAgent>().isStopped = false;
        }
    }
    
    public void FreezeZombies(){
        GameObject[] zombies ;
        zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach(GameObject zombie in zombies){
            zombie.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    public void FinishGame(){
        DestroyZombies();
        finishGameCanvas.SetActive(true);
        playerController.enabled = false;
        pistol.enabled = false;
    }

    public void ActiveFinishTrigger(){
        finishTrigger.SetActive(true);
    }
    
    public void DeactiveFinishTrigger(){
        finishTrigger.SetActive(false);
    }
}
