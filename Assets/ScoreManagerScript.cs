using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManagerScript : MonoBehaviour
{
    public float Propaganda = 100;
    public int Podrska = 10;

    [SerializeField] private TMP_Text propagandaText;
    [SerializeField] private TMP_Text podrskaText;

    public static ScoreManagerScript Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Podrska = DifficultyLevelScript.Instance.startingSupport;
    }

    public void Update()
    {
        Propaganda -= Time.deltaTime / 2;
        propagandaText.text = ((int)Propaganda).ToString();
        podrskaText.text = Podrska.ToString();
        if(Propaganda <= 0)
        {
            SceneManager.LoadScene("Victory Scene");
        }
    }
}
