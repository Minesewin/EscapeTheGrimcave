using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TImeCounter : MonoBehaviour
{
    private static TImeCounter _instance;
    private static float totalTimePlayed = 0f;
    [SerializeField] TextMeshProUGUI timerText;

    private void Awake()
    {
        // Singleton pattern
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            // Retrieve the total time played from PlayerPrefs
            totalTimePlayed = PlayerPrefs.GetFloat("TotalTimePlayed", 0f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Update the timer every frame
        totalTimePlayed += Time.deltaTime;

        float rounded = (float)Math.Round(totalTimePlayed, 2);
        timerText.text = rounded.ToString();
        Debug.Log(totalTimePlayed);

        // Save the total time played in PlayerPrefs
        PlayerPrefs.SetFloat("TotalTimePlayed", totalTimePlayed);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        // Reset the total time played when the application is closed
        PlayerPrefs.SetFloat("TotalTimePlayed", 0f);
        PlayerPrefs.Save();
    }
}
