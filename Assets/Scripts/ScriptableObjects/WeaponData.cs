using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string Name;
    public int MagazineSize;
    public int MaxBullets;
    public int StartingBullets;
    public float ReloadTime;
    public float FireRate;
    public GameObject ShotPrefab;
    public int WeaponSlot;
}