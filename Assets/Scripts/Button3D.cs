using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3D : MonoBehaviour
{
    [SerializeField]
    private string _id;


    [SerializeField]
    private AnimationCurve animCurve;

    [SerializeField]
    private float animSpeed;

    [SerializeField]
    private float animDuration;

    [SerializeField]
    private float animStep;


    private Vector3 defaultPos;

    private void Start()
    {
        defaultPos = transform.position;
    }


    IEnumerator playAnim()
    {
        float counter = animDuration;

        while (counter > 0f)
        {
            transform.position = new Vector3(defaultPos.x, defaultPos.y + animCurve.Evaluate((animDuration - counter) /animDuration)*1.2f   , defaultPos.z);
            counter -= Time.deltaTime * animSpeed;
            yield return null;
        }

        transform.position = defaultPos;
    }

    public string pressAnim()
    {
        StopCoroutine("playAnim");
        transform.position = defaultPos;
        StartCoroutine(playAnim());

        return _id;
    }

}
