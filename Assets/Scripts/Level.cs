using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public float timeToDie;

    void Start()
    {
        GameEvents.current.onLeaveGameArea += Reload;

        Debug.Log("Level Start");
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().PlayMusic();
        timeToDie = 2f;
    }

    void Reload()
    {
        if (timeToDie > 0)
        {
            timeToDie -= Time.deltaTime;
        }
        else
        {
            GameEvents.current.onLeaveGameArea -= Reload;
            Scene currentLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentLevel.name);
        }
    }
}
