using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public string playerName;
    public string highestScore;
    public string highestScorePlayerName;

    public TMP_InputField nameInput;
    public Button startGame;

    private void Start()
    {
        startGame.onClick.AddListener(StartGame);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }

    public void StartGame()
    {
        if (!string.IsNullOrWhiteSpace(nameInput.text))
        {
            playerName = nameInput.text;

            SceneManager.LoadScene(1);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string highestScore;
        public string highestScorePlayerName;
    }
  
    public void SaveScore(int score)
    {
        highestScore = score.ToString();
        highestScorePlayerName = playerName;

        SaveData saveData = new SaveData();
        saveData.highestScorePlayerName = highestScorePlayerName;
        saveData.highestScore = highestScore;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/scoretable.json", json);
    }

    public void LoadScore()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/scoretable.json");

        SaveData data = JsonUtility.FromJson<SaveData>(json);

        highestScorePlayerName = data.highestScorePlayerName;
        highestScore = data.highestScore;
    }
}
