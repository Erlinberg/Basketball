using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreController : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textMP;

    private int playerScore = 0;
    private int aiScore = 0;

    private void Start()
    {
        textMP = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void updateTMP()
    {
        textMP.text = string.Format("{0}:{1}", playerScore, aiScore);
    }

    public void addPlayerScore(int amount)
    {
        playerScore++;
        updateTMP();
    }
    
    public void addAIScore(int amount)
    {
        aiScore++;
        updateTMP();
    }

    public void resetScore()
    {
        playerScore = 0;
        aiScore = 0;
        updateTMP();
    }
}
