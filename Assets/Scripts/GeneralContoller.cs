using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class GeneralContoller : MonoBehaviour
{
    [SerializeField]
    private Image blockFillerUI;

    [SerializeField]
    private Myach myach;

    [SerializeField]
    private ScoreController scrCnt;

    [SerializeField]
    private TMPro.TextMeshProUGUI currText;

    public float myachMaxDistance;


    [HideInInspector]
    public bool canPress = true;

    private Coroutine pressBlockCor;

    [HorizontalLine(color: EColor.White)]

    [ReadOnly]
    public bool workAI = true;

    [ReadOnly]
    public float[] distanceRange;

    [ReadOnly]
    public string pressedButton;

    [ReadOnly]
    public string currentHoleID = null;

    [HorizontalLine(color: EColor.White)]
    public Gradient myachGrad;

    public Vector3 kickOffForce;

    public float deviationForce = 2f;

    [HorizontalLine(color: EColor.White)]
    public float aiReactionTime;
    public float aiKickChance;
    public float aiSuccsesfulKickChance;

    [SerializeField]
    private float aiReactionTimeDecreaseRate;
    [SerializeField]
    private float aiKickChanceIncreaseRate;
    [SerializeField]
    private float aiSuccsesfulKickChanceIncreaseRate;

    [HorizontalLine(color: EColor.White)]

    public int pointsToWinRound;

    [SerializeField]
    private float blockDuration;

    [ColorUsageAttribute(false, true)]
    public Color inactiveColor;

    [ColorUsageAttribute(false, true)]
    public Color activeColor;

    private void Start()
    {
        distanceRange = myachGrad.colorKeys.Select(colorKey => (colorKey.time * myachMaxDistance)).ToArray();
    }

    IEnumerator blockTimer()
    {
        canPress = false;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            blockFillerUI.fillAmount = 1 - normalizedTime;
            normalizedTime += Time.deltaTime / blockDuration;
            yield return null;
        }

        canPress = true;
    }

    public void blockPress()
    {
        if (!(pressBlockCor == null)) { StopCoroutine(pressBlockCor); } 
        pressBlockCor = StartCoroutine(blockTimer());
    }

    public void restartGame()
    {
        StartCoroutine(myach.resetBall(0f));
        scrCnt.resetScore();
    }

    public void setCurrentHoleID(string value)
    {
        currentHoleID = value;
        currText.text = currentHoleID;
    }


    public void setAIMode(bool state) { workAI = state; }

    private IEnumerator setKeyDown(string p_k)
    {
        pressedButton = p_k;
        yield return new WaitForSeconds(0.05f);
        pressedButton = null;
    }

    public void setButton(string p_k)
    {
        StopCoroutine("setKeyDown");
        StartCoroutine(setKeyDown(p_k));
    }


    public void levelUp()
    {
        aiReactionTime /= aiReactionTimeDecreaseRate;
        aiKickChance *= aiKickChanceIncreaseRate;
        aiSuccsesfulKickChance *= aiSuccsesfulKickChanceIncreaseRate;

        scrCnt.resetScore();
        scrCnt.generalScore += 1;
    }

    public void loose()
    {
        // pass
    }




}
