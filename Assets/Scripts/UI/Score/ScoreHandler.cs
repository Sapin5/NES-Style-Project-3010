using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private Text[] scoreLabels;

    private int score = 0;
    const int MAX_NUM_DIGITS = 6;

    public void UpdateScore(int score) {
        this.score += score;

        int numZerosPrior;

        if (this.score < 100) {
            numZerosPrior = MAX_NUM_DIGITS - 2;
        } else if (this.score < 1000) {
            numZerosPrior = MAX_NUM_DIGITS - 3;
        } else {
            numZerosPrior = MAX_NUM_DIGITS - 4;
        }

        foreach (var label in scoreLabels) {
            string newLabel = "";
        
            for (int i = 0; i < numZerosPrior; i++)
                newLabel += "0";

            newLabel += this.score.ToString();
            label.text = newLabel;
        }
    }

    public int GetScore()  => this.score;

}
