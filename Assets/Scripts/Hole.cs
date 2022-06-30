using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public int _id = 0;

    [SerializeField]
    private GeneralContoller generalCnt;

    [SerializeField]
    private Transform holeCenter;

    private float reactionTimer = 0;


    private void OnCollisionStay(Collision collision)
    {
        if (!(collision.gameObject.tag == "myach")) { return; }

        float distance = Vector3.Distance(holeCenter.position, collision.transform.position);
        Debug.DrawLine(holeCenter.position, collision.transform.position, generalCnt.myachGrad.Evaluate(distance / generalCnt.myachMaxDistance), 0f, false);

        int val = (_id + 1);
        if (Input.GetKey(val.ToString()) && generalCnt.canPress)
        {
            if (distance < generalCnt.distanceRange[0]) 
            { 
                collision.gameObject.GetComponent<Myach>().setTargetAI(); 
                generalCnt.blockPress(); 
            }
            else
            {
                Vector3 posVector = Quaternion.AngleAxis(180, Vector3.up) * (collision.transform.position - holeCenter.position);
                Vector3 forceVec = Vector3.Scale(generalCnt.kickOffForce, posVector);

                collision.gameObject.GetComponent<Rigidbody>().AddForce(forceVec, ForceMode.Impulse);
            }
        }
        else if (generalCnt.workAI)
        {
            if (distance < generalCnt.distanceRange[0])
            {
                reactionTimer += Time.deltaTime;

                if (reactionTimer >= generalCnt.aiReactionTime)
                {
                    collision.gameObject.GetComponent<Myach>().setTargetPlayer();
                }
            }
            else
            {
                reactionTimer = 0;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "myach") { reactionTimer = 0; }

    }

}
