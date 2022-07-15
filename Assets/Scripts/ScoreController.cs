using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreController : MonoBehaviour
{
    public int generalScore = 250;

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
        generalCnt.audioCnt.scoreChange();
        playerScore++;

        if (playerScore >= generalCnt.pointsToWinRound)
        {
            generalCnt.levelUp();
            generalScore += (playerScore - aiScore)*14;
            resetScore();
            return;
        }
        updateTMP();
    }
    
    public void addAIScore(int amount)
    {
        generalCnt.audioCnt.scoreChange();
        aiScore++;

        if (aiScore >= generalCnt.pointsToWinRound)
        {
            generalCnt.loose();
            resetScore();
            return;
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
