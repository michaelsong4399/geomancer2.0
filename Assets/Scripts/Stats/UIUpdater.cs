using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    TextMeshProUGUI tmp;
    StatsRecorder stats;
    
    // Start is called before the first frame update
    void Start()
    {
        tmp = gameObject.GetComponent<TextMeshProUGUI>();
        stats = GameObject.Find("StatsManager").GetComponent<StatsRecorder>();
        tmp.text = "Score: 0";
    }

    public void updateText()
    {
        tmp.text = "Score: " + stats.getScore();
    }
}
