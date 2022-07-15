using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCnt : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI text;

    [SerializeField]
    private Scrollbar sld;    
    
    [SerializeField]
    private Toggle toggleMusicObj;    
    
    [SerializeField]
    private Toggle toggleEffectsObj;

    [SerializeField]
    private Data data;

    public void sliderOnChange()
    {
        text.text = (10f*sld.value + 1).ToString();

        data.difficultyLevel = (int) Mathf.Round(10f * sld.value + 1);
    }

    public void toggleMusic()
    {
        data.musicOn = toggleMusicObj.isOn;
    }

    public void toggleEffects()
    {
        data.effectsOn = toggleEffectsObj.isOn;
    }
}
