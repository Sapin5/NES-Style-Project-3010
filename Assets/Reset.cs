using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    string currentSceneName;
    private void Awake() {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        SceneManager.LoadScene("BigBadBoss");
    }
}
