using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Myach : MonoBehaviour
{
    [SerializeField]
    private ScoreController scoreCnt;
    [SerializeField]
    private GameObject effectHit;
    [SerializeField]
    private Transform targetAI;
    [SerializeField]
    private Transform targetPlayer;

    [HorizontalLine(color: EColor.White)]

    [SerializeField]
    private Vector3 startPosition;



    private bool cooldown = false;
    private ThrowSimulation throwSim;
    private Rigidbody rb;

    private IEnumerator cooldownPress()
    {
        cooldown = true;
        yield return new WaitForSeconds(1f);
        cooldown = false;
    }

    public void setTargetAI()
    {
        if (cooldown) { return; }
        throwSim.throwBallRB(targetAI);
        StartCoroutine(cooldownPress());
    }

    public void setTargetPlayer()
    {
        if (cooldown) { return; }
        throwSim.throwBallRB(targetPlayer);
        StartCoroutine(cooldownPress());
    }

    public int RandomSign()
    {
        return Random.value < .5 ? 1 : -1;
    }

    private void Start()
    {
        throwSim = GetComponent<ThrowSimulation>();

        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Mathf.Round(Random.value) * RandomSign() * 1f, 0f, Mathf.Round(Random.value) * RandomSign() * 1f), ForceMode.Impulse);
    }

    public IEnumerator resetBall(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        rb.AddForce(new Vector3(Mathf.Round(Random.value) * RandomSign() * 1f, 0f, Mathf.Round(Random.value) * RandomSign() * 1f), ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        if (rb.IsSleeping())
            rb.WakeUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "basket") 
        {
            string name = other.gameObject.name; 

            if (name == "playerBasket")
            {
                scoreCnt.addAIScore(1);
                Destroy(Instantiate(effectHit, transform), 1f);
            }
            else if (name == "aiBasket")
            {
                scoreCnt.addPlayerScore(1);
                Destroy(Instantiate(effectHit, transform), 1f);
            }
        }

        //StartCoroutine(resetBall(0f));
    }


    [SerializeField]
    private GameObject badTail;
    
    [SerializeField]
    private GameObject goodTail;

    public void badKick()
    {
        Destroy(Instantiate(badTail, transform), 1f);
    }

    public void perfectKick()
    {
        Destroy(Instantiate(goodTail, transform), 1f);
    }

}
