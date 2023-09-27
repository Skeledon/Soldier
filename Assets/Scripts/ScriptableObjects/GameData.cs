using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameData : ScriptableObject
{
    public int TargetScore;
    public int NumberOfSoldiersPerTeam;
    public int TimeOfRound;
}
