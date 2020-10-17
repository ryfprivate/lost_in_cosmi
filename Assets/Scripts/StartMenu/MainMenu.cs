using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().PlayMusic();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level01");
    }
}
