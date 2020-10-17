using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public float timeToDie;
    private bool isDying;

    void Start()
    {
        GameEvents.current.onLeaveGameArea += Die;
        GameEvents.current.onObstacleCollision += Die;

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
}
