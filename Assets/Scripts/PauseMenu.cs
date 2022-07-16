using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GeneralContoller generalCnt;

    [SerializeField]
    private GameObject panel;

    public void Resume()
    {
        generalCnt.isPaused = false;
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        generalCnt.scene.loadlevel("Menu");
    }

    private void Start()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            generalCnt.isPaused = !generalCnt.isPaused;
            panel.SetActive(generalCnt.isPaused);
            Time.timeScale = generalCnt.isPaused ? 0 : 1;
        }
    }
}
