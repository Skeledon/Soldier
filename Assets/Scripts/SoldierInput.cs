using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierInput : MonoBehaviour
{
    private Camera mainCam;

    [SerializeField]
    private SoldierController myController;

    [SerializeField]
    private int playerID;

    private Rewired.Player myPlayer;

    private void Awake()
    {
        myPlayer = Rewired.ReInput.players.GetPlayer(playerID);
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        myController.Move(new Vector2(myPlayer.GetAxis("MoveHorizontal"), myPlayer.GetAxis("MoveVertical")));
        myController.RotateAimTowards(mainCam.ScreenToWorldPoint(Input.mousePosition));
        if(myPlayer.GetButton("Fire"))
        {
            myController.Shoot();
        }
    }
}
