using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DisplayScores : MonoBehaviour
{
    public TMP_Text displayedNames;
    public TMP_Text displayedScores;
    // Start is called before the first frame update
    void Start()
    {
        displayedNames.text = Scoreboard.Instance.stringOfNames();
        displayedScores.text = Scoreboard.Instance.stringOfScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
