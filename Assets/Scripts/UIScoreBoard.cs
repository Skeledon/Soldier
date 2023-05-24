using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreBoard : MonoBehaviour
{
    [SerializeField]
    private TMP_Text redScoreText;

    [SerializeField]
    private TMP_Text blueScoreText;

    [SerializeField]
    private TMP_Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PrintTime(float seconds)
    {
        System.TimeSpan ts = System.TimeSpan.FromSeconds(seconds);
        timerText.text = string.Format("{0:0}:{1:00}", ts.TotalMinutes, ts.Seconds);
    }

    public void ChangeRedScore(int score)
    {
        redScoreText.text = score.ToString();
    }

    public void ChangeBlueScore(int score)
    {
        blueScoreText.text = score.ToString();
    }
}
