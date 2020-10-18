using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public Animator transition;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    public float timeToDie;
    private bool isDying;

    void Awake()
    {
        xMin = -10;
        xMax = 30;
        yMin = -10;
        yMax = 10;
    }

    void Start()
    {
        GameEvents.current.onLeaveGameArea += Die;
        GameEvents.current.onObstacleCollision += Die;
        GameEvents.current.onDestinationCollision += LoadNextLevel;

        Debug.Log("Level Start");
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().PlayMusic();
        timeToDie = 2f;
        isDying = false;
    }

    void Update()
    {
        if (isDying)
        {
            if (timeToDie > 0)
            {
                timeToDie -= Time.deltaTime;
            }
            else
            {
                Reload();
            }
        }
    }

    void Die()
    {
        isDying = true;
    }

    void Reload()
    {
        GameEvents.current.onLeaveGameArea -= Reload;
        Scene currentLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentLevel.name);
    }

    void LoadNextLevel(GameObject dest)
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play animation
        transition.SetTrigger("Start");
        // Wait
        yield return new WaitForSeconds(1);

        // Load scene
        SceneManager.LoadScene(levelIndex);
    }
}
