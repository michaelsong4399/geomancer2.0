using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    TextMeshProUGUI tmp;
    public Image screenOverlay;
    public TextMeshProUGUI gameOverText;
    StatsRecorder stats;
    SaveSerial save;
    
    // Start is called before the first frame update
    void Start()
    {
        tmp = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(LateStart(0.1f));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        init();
    }
    void init ()
    {
        stats = GameObject.Find("StatsManager").GetComponent<StatsRecorder>();
        save = GameObject.Find("StatsManager").GetComponent<SaveSerial>();
        updateText();
    }
    public void updateText()
    {
        tmp.text = "Score: " + stats.getScore() + "\n" + "High Score: " + save.getHighScore();
    }
    public void updateOverlay(Color32 c)
    {
        screenOverlay.color = c;
    }
    public void gameOver()
    {
        if (save.getHighScore() < stats.getScore())
        {
            gameOverText.text = "Game Over" + "\n" + "Score: " + stats.getScore() + "\n" + "New High Score!";
        }
        else
        {
            gameOverText.text = "Game Over" + "\n" + "Score: " + stats.getScore() + "\n" + "High Score: " + save.getHighScore();
        }
    }
}
