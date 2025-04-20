using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KoNeSkaceScript : MonoBehaviour
{
    private float lastUsedTime;
    [SerializeField] private float cooldown = 20;
    [SerializeField] private int supportGain = 2;
    private bool isActive = true;
    [SerializeField] private Image image;

    public static event Action OnKoNeSkace;

    public void Start()
    {
        lastUsedTime = Time.realtimeSinceStartup - cooldown;
    }

    public void OnButtonClick()
    {
        if(lastUsedTime + cooldown < Time.realtimeSinceStartup)
        {
            OnKoNeSkace?.Invoke();
            ScoreManagerScript.Instance.Podrska += supportGain;
            lastUsedTime = Time.realtimeSinceStartup;
            isActive = false;
            image.color = Color.red;
        }
    }

    public void Update()
    {
        if((lastUsedTime + cooldown < Time.realtimeSinceStartup) && !isActive)
        {
            isActive = true;
            image.color = Color.green;
        }
    }
}
