using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public int _id = 0;

    [SerializeField]
    private GeneralContoller generalCnt;

    [SerializeField]
    private Vector3 holeCenterOffset;

    private Vector3 holeCenter;

    private float reactionTimer = 0;

    private void Start()
    {
        holeCenter = (transform.position + holeCenterOffset);
        _id = int.Parse(name.Substring(name.Length - 1));
    }


    private void OnCollisionStay(Collision collision)
    {
        if (!(collision.gameObject.tag == "myach")) { return; }

        float distance = Vector3.Distance(holeCenter, collision.transform.position);
        Debug.DrawLine(holeCenter, collision.transform.position, generalCnt.myachGrad.Evaluate(distance / generalCnt.myachMaxDistance), 0f, false);

        if (distance > generalCnt.distanceRange[1]) { return; }

        if (Input.GetKey(_id.ToString()) && generalCnt.canPress)
        {
            if (distance < generalCnt.distanceRange[0]) 
            { 
                collision.gameObject.GetComponent<Myach>().setTargetAI(); 
                generalCnt.blockPress(); 
            }
            else
            {
                Vector3 posVector = Quaternion.AngleAxis(180, Vector3.up) * (collision.transform.position - holeCenter);
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
