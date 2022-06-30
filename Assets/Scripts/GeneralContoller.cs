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

    [HideInInspector]
    public bool canPress = true;

    [HideInInspector]
    public bool workAI = true;

    
    public void setAIMode(bool state) { workAI = state; }

    private Coroutine pressBlockCor;

    [HorizontalLine(color: EColor.White)]

    [ReadOnly]
    public float[] distanceRange;

    [HorizontalLine(color: EColor.White)]
    public Gradient myachGrad;
    public float myachMaxDistance;

    public Vector3 kickOffForce;
    public float aiReactionTime;

    [SerializeField]
    private float blockDuration;

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
}
