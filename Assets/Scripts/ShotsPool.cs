using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotsPool
{
    private Stack<GameObject> availableShots;
    private List<GameObject> activeShots;
    private GameObject shotPrefab;
    private int currentlyActiveShots;
    private GameObject shotsFather;

    private const int SHOT_THRESHOLD = 20;

    public void Init(int startingShots, GameObject sPrefab, GameObject shotsFather)
    {
        availableShots = new Stack<GameObject>();
        activeShots = new List<GameObject>();
        shotPrefab = sPrefab;
        this.shotsFather = shotsFather;
        AddShotsToPool(startingShots);
        CheckMinimumShotCount();
    }

    public void ShotFired(Vector3 position, Quaternion rotation, GameObject owner)
    {
        GameObject shot = availableShots.Pop();
        activeShots.Add(shot);
        shot.transform.SetPositionAndRotation(position, rotation);
        shot.SetActive(true);
        shot.GetComponent<Shot>().Init(owner, this);
        CheckMinimumShotCount(); //failsafe mechanism in case the pool runs out of shots. It shouldn't happen but better safe than sorry.
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
            GameObject tmp = GameObject.Instantiate(shotPrefab, shotsFather.transform);
            tmp.SetActive(false);
            availableShots.Push(tmp);
        }
    }

    private void CheckMinimumShotCount()
    {
        //adds shots in the pool in case the number falls below a certain threshold to avoid the pool runing out of shots.
        if (availableShots.Count <= SHOT_THRESHOLD)
            AddShotsToPool(SHOT_THRESHOLD);
    }
}
