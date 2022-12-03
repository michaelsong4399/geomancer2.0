using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsRecorder : MonoBehaviour
{
    private int numThrown = 0;
    private int numHit = 0;
    private int score = 0;
    private int highScore = 0;
    public UIUpdater ui;

    public void rockThrown ()
    {
        numThrown++;
    }

    public void hit()
    {
        numHit++;
    }

    public void increaseScore(int points)
    {
        score += points;
        ui.updateText();
    }

    public int getScore()
    {
        return score;
    }

    public float getAccuracy()
    {
        if (numThrown > 0)
        {
            return (float)numHit/numThrown;
        }
        else
        {
            return 0f;
        }
    }
}
