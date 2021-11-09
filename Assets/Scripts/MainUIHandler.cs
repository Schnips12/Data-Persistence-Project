using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MainUIHandler : MonoBehaviour
{
    public TMP_Text congratulations;

     // Start is called before the first frame update
    void Start()
    {
        if (Scoreboard.Instance.IsNewBest())
        {
            congratulations.text = "New best score: " + Scoreboard.Instance.lastScore + "!";
            congratulations.enabled = true;
            GameObject.Find("Save Score").SetActive(true);
            GameObject.Find("Player Name").SetActive(true);
        } else
        {
            congratulations.enabled = false;
            GameObject.Find("Save Score").SetActive(false);
            GameObject.Find("Player Name").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void NewGame()
    {    
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void Leaderboard()
    {    
        SceneManager.LoadScene("Scoreboard", LoadSceneMode.Single);
    }

    public void SaveScore()
    {
        string name = GameObject.Find("Player Name").GetComponent<TMP_InputField>().text;
        Scoreboard.Instance.AddScore(name, Scoreboard.Instance.lastScore);
        GameObject.Find("Save Score").SetActive(false);
        GameObject.Find("Player Name").SetActive(false);
    }
}
