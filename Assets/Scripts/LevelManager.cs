using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    LevelStartupData startupData;

    [SerializeField]
    Transform playerStartingPoint;

    [SerializeField]
    UIController interfaceController;

    // Start is called before the first frame update
    void Start()
    {
        InstantiatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiatePlayer()
    {
        GameObject playerGo = Instantiate(startupData.PlayerPrefab, playerStartingPoint.position, Quaternion.identity);
        LinkPlayerInterface(playerGo);
        playerGo.GetComponent<SoldierController>().Spawn();
    }

    private void LinkPlayerInterface(GameObject playerGo)
    {
        interfaceController.InitializeUILinks(playerGo);
    }
}
