using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public float timeCount = 0;
    public TextMeshProUGUI timeText;

    public string targetSceneName = "MainMenu";

    private void Start()
    {
        if (PlayerPrefs.HasKey("timeValue"))
        {
            timeCount = PlayerPrefs.GetFloat("timeValue");
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == targetSceneName)
        {
            // Perform your action or behavior specific to this scene
            PlayerPrefs.DeleteAll();
        }
        else if (SceneManager.GetActiveScene().name != targetSceneName)
        {
            timeCount += Time.deltaTime;
            PlayerPrefs.SetFloat("timeValue", timeCount);
            Debug.Log(timeCount);

            float rounded = (float)Math.Round(timeCount, 2);
            timeText.text = rounded.ToString();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            PlayerPrefs.DeleteAll();
        }

    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
