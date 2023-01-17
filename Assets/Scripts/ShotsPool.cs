using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotsPool
{
    private Stack<GameObject> availableShots;
    private List<GameObject> activeShots;
    private GameObject shotPrefab;
    private int currentlyActiveShots;

    private const int SHOT_THRESHOLD = 20;

    public void Init(int startingShots, GameObject sPrefab)
    {
        availableShots = new Stack<GameObject>();
        activeShots = new List<GameObject>();
        shotPrefab = sPrefab;
        AddShotsToPool(startingShots);
    }

    public void ShotFired(Vector3 position, Quaternion rotation, GameObject owner)
    {
        GameObject shot = availableShots.Pop();
        activeShots.Add(shot);
        shot.transform.SetPositionAndRotation(position, rotation);
        shot.SetActive(true);
        shot.GetComponent<Shot>().Init(owner, this);
        CheckMinimumShotCount();
    }

    public void ShotDestroyed(GameObject shot)
    {
        activeShots.Remove(shot);
        availableShots.Push(shot);
        shot.SetActive(false);
    }

    private void AddShotsToPool(int numberOfShots)
    {
        for (int i = 0; i < numberOfShots; i++)
        {
            GameObject tmp = GameObject.Instantiate(shotPrefab);
            tmp.SetActive(false);
            availableShots.Push(tmp);
        }
    }

    private void CheckMinimumShotCount()
    {
        if (availableShots.Count <= SHOT_THRESHOLD)
            AddShotsToPool(SHOT_THRESHOLD);
    }
}
