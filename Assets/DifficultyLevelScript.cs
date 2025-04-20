using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyLevelScript : MonoBehaviour
{
    public int startingSupport = 30;
    public float redarMaxEnergy = 50;
    public int policeDamage = 5;

    public static DifficultyLevelScript Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }


    public void StartEasyGame()
    {
        startingSupport = 50;
        redarMaxEnergy = 80;
        policeDamage = 5;
        SceneManager.LoadScene("Game Scene");
    }

    public void StartMediumGame()
    {
        startingSupport = 30;
        redarMaxEnergy = 50;
        policeDamage = 5;
        SceneManager.LoadScene("Game Scene");
    }

    public void StartHardGame()
    {
        startingSupport = 20;
        redarMaxEnergy = 40;
        policeDamage = 8;
        SceneManager.LoadScene("Game Scene");
    }
}
