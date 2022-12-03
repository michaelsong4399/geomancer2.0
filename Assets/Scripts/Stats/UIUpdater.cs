using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    TextMeshProUGUI tmp;
    StatsRecorder stats;
    int highScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        tmp = gameObject.GetComponent<TextMeshProUGUI>();
        stats = GameObject.Find("StatsManager").GetComponent<StatsRecorder>();
        this.highScore = GameObject.Find("StatsManager").GetComponent<SaveSerial>().getHighScore();
        updateText();
    }
    public void updateText()
    {
        tmp.text = "Score: " + stats.getScore() + "\n" + "High Score: " + highScore;
    }
}
