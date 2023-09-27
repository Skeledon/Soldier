using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    LevelStartupData startupData;

    private int targetScore;
    public int RedScore { get; private set; }
    public int BlueScore { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncreaseRedScore(int amount)
    {
        RedScore += amount;
    }

    private void IncreaseRedScore()
    {
        IncreaseRedScore(1);
    }

    private void IncreaseBlueScore(int amount)
    {
        BlueScore += amount;
    }

    private void IncreaseBlueScore()
    {
        IncreaseBlueScore(1);
    }


}
