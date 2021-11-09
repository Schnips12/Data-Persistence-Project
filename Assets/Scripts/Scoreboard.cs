using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class Scoreboard : MonoBehaviour
{
    public static Scoreboard Instance;
    private AllScores data;
    public long lastScore;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadScores();
        } else
        {
            Destroy(gameObject);
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(string name, long score)
    {
        NameAndScore toAdd = new NameAndScore();
        toAdd.playerName = name;
        toAdd.playerScore = score;

        data.listOfScores.Add(toAdd);
        data.listOfScores.Sort();

        while (data.listOfScores.Count > 10)
        {
            data.listOfScores.RemoveAt(10);
        }

        SaveScores();
    }

    public bool IsNewBest()
    {
        if(data.listOfScores.Count > 0)
        {
            return lastScore > data.listOfScores[0].playerScore;
        } else if (lastScore == 0)
        {
            return false;
        } else
        {
            return true;
        }
    }

    // Save the scoreboard to a local file.
    public void SaveScores()
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Read the scoreboard from a local file.
    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<AllScores>(json);
        } else
        {
            data = new AllScores();
        }
    }

    public string BestScore()
    {
        string leader;
        if (data.listOfScores.Count == 0)
        {
            leader =  "................";
        } else
        {
            leader =  data.listOfScores[0].playerName + " " + data.listOfScores[0].playerScore;
        }
        return leader;
    }

    // Structure for saving one score with the player name.
    [System.Serializable]
    class NameAndScore : IComparable<NameAndScore>
    {
        public string playerName;
        public long playerScore;
        public int CompareTo(NameAndScore toCompare)
        {
            return toCompare.playerScore.CompareTo(playerScore);
        }
    }

    // Structure for saving the complete scoreboard
    [System.Serializable]
    class AllScores
    {
        public List<NameAndScore> listOfScores;

        public AllScores()
        {
            listOfScores = new List<NameAndScore>();
        }
    }
}
