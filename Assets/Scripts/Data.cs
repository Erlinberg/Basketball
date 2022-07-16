using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public int difficultyLevel;
    public int generalScore;
    public bool musicOn;
    public bool effectsOn;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
