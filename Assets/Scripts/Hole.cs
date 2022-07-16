using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public int _id = 0;

    [SerializeField]
    private GeneralContoller generalCnt;

    [SerializeField]
    private Transform holeCenterObj;

    private Vector3 holeCenter;

    private float reactionTimer = 0;

    private void Start()
    {
        holeCenter = holeCenterObj.position;
        _id = int.Parse(name.Substring(name.Length - 1));
    }

    private void OnCollisionEnter(Collision collision)
    {
        generalCnt.setCurrentHoleID(name.Substring(name.Length - 2));
    }


    void setBallTarget(string target, GameObject ball, float distance)
    {
        if (target == "AI") ball.GetComponent<Myach>().setTargetAI();
        else ball.GetComponent<Myach>().setTargetPlayer();

        generalCnt.blockPress();

        Vector3 posVector = Quaternion.AngleAxis(180, Vector3.up) * (ball.transform.position - holeCenter);
        Vector3 forceVec = generalCnt.deviationForce * (distance / generalCnt.myachMaxDistance) * posVector;

        ball.GetComponent<Rigidbody>().AddForce(forceVec, ForceMode.Impulse);
        ball.GetComponent<Myach>().perfectKick();
    }

    void setBallForce(GameObject ball)
    {
        Vector3 posVector = Quaternion.AngleAxis(180, Vector3.up) * (ball.transform.position - holeCenter);
        Vector3 forceVec = Vector3.Scale(generalCnt.kickOffForce, posVector);

        ball.gameObject.GetComponent<Rigidbody>().AddForce(forceVec, ForceMode.Impulse);
        generalCnt.blockPress();

        ball.GetComponent<Myach>().badKick();
    }


    private void OnCollisionStay(Collision collision)
    {
        if (!(collision.gameObject.tag == "myach")) { return; }
        GameObject ball = collision.gameObject;

        float distance = Vector3.Distance(holeCenter, ball.transform.position);

        if (distance > generalCnt.distanceRange[1]) { return; }

        if (distance < generalCnt.distanceRange[0])
        {
            ball.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", generalCnt.activeColor);
        }

        if ((Input.GetKey(_id.ToString()) || generalCnt.pressedButton == name.Substring(name.Length - 2)) && generalCnt.canPress)
        {
            generalCnt.audioCnt.ballPushed();
            if (distance < generalCnt.distanceRange[0]) 
            {
                setBallTarget("AI", ball, distance);
            }
            else
            {
                setBallForce(ball);
            }
        }

        else if (generalCnt.workAI)
        {
            reactionTimer += Time.deltaTime;

            if (reactionTimer <= generalCnt.aiReactionTime) { return; }

            if (Random.Range(0, 100) > generalCnt.aiKickChance) { reactionTimer = 0; return; }

            generalCnt.audioCnt.ballPushed();
            if (Random.Range(0, 100) <= generalCnt.aiSuccsesfulKickChance) { setBallTarget("player", ball, distance); reactionTimer = 0; }
            else { setBallForce(ball); reactionTimer = 0; }
        }
        else
        {
            reactionTimer = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "myach") { reactionTimer = 0; }
        collision.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", generalCnt.inactiveColor);

        //generalCnt.setCurrentHoleID("--");
    }

}
