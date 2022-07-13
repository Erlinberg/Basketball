using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreController : MonoBehaviour
{
    public int generalScore = 0;

    [SerializeField]
    private GeneralContoller generalCnt;

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

        if (playerScore >= generalCnt.pointsToWinRound)
        {
            generalCnt.levelUp();
        }
        updateTMP();
    }
    
    public void addAIScore(int amount)
    {
        aiScore++;

        if (aiScore >= generalCnt.pointsToWinRound)
        {
            // pass
        }
        updateTMP();
    }

    public void resetScore()
    {
        playerScore = 0;
        aiScore = 0;
        updateTMP();
    }
}
