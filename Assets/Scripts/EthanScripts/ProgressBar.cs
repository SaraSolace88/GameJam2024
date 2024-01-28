using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider ProgressSlider;
    [SerializeField] private RhythmSystem rs;
    [SerializeField] private SongStats ss;

    public float progressValue;

    private void Start()
    {
        ss = GameObject.FindGameObjectWithTag("Song").GetComponent<SongStats>();
    }

    private void Update()
    {
        if(rs.songState)
        {
            progressValue = Mathf.Clamp01(rs.songPosInBeats/ss.TotalBeats);
            ProgressSlider.value = progressValue;
        }
    }
}
