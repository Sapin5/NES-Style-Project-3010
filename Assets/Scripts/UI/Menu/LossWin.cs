using UnityEngine.SceneManagement;
using UnityEngine;

public class LossWin : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private GameObject LossScreen;
    [SerializeField] private string WinScreen;

    [SerializeField] private Health bossHealth;

    private float timer;
    private void Awake(){
        if(health == null){
            health = FindAnyObjectByType<PlayerHealth>();
        }
        if(bossHealth == null){
            bossHealth = FindAnyObjectByType<DraculaStateHandler>().GetComponent<Health>();
        }
    }

    private void Update(){
        if(health.RemainingHealth() <= 0){
            LossScreen.SetActive(true);
        }

        if(bossHealth.GetHealth() <= 0){
            Timer();
        }
    }

    private void Timer(){
        timer+=Time.deltaTime;

        if(timer > 3f){
            SceneManager.LoadScene(WinScreen);
        }
    }
}
