using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text energyText;
    [SerializeField] AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] IOSNotificationHandler iosNotificationHandler;
    [SerializeField] int maxEnergy;
    [SerializeField] int energyRechargeDuration;

    int energy;

    const string EnergyKey = "Energy";
    const string EnergReadyKey = "EnergyReady";

    private void Start()
    {
        UpdateHighScore();

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if (energy < maxEnergy)
        {
            CheckEnergy();
        }

        energyText.text = $"Play ({energy})";
    }

    private void Update()
    {
        if (energy < maxEnergy)
        {
            CheckEnergy();

            energyText.text = $"Play ({energy})";
        }
    }

    public void Play()
    {
        if (energy < 1) return;

        string energyReadyString = PlayerPrefs.GetString(EnergReadyKey, string.Empty);

        if (energyReadyString == string.Empty)
        {
            UpdateEnergy();
        }

        energy--;

        PlayerPrefs.SetInt(EnergyKey, energy);

        SceneManager.LoadScene(1);
    }

    private void UpdateHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt(ScoreHandler.HighScoreKey, 0);

        highScoreText.text = $"High Score: {currentHighScore}";
    }

    private void CheckEnergy()
    {
        string energyReadyString = PlayerPrefs.GetString(EnergReadyKey, string.Empty);

        if (energyReadyString == string.Empty) return;

        DateTime energyReady = DateTime.Parse(energyReadyString);

        if (DateTime.Now > energyReady)
        {
            energy++;

            PlayerPrefs.SetInt(EnergyKey, energy);

            if (energy < maxEnergy)
            {
                UpdateEnergy();
            }
            else
            {
                PlayerPrefs.SetString(EnergReadyKey, string.Empty);
            }
        }
    }

    private void UpdateEnergy()
    {
        DateTime duration = DateTime.Now.AddMinutes(energyRechargeDuration);

        PlayerPrefs.SetString(EnergReadyKey, duration.ToString());

#if UNITY_ANDROID
        androidNotificationHandler.ScheduleNotification(duration);
#elif UNITY_IOS
        iosNotificationHandler.ScheduleNotification(energyRechargeDuration);
#endif
    }
}
