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

    [SerializeField]
    private bool relativeMovement = false;

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
        float inputX = myPlayer.GetAxis("MoveHorizontal");
        float inputY = myPlayer.GetAxis("MoveVertical");
        myController.Move(new Vector2(inputX, inputY), relativeMovement);
        myController.RotateAimTowards(mainCam.ScreenToWorldPoint(Input.mousePosition));
        if(myPlayer.GetButton("Fire"))
        {
            myController.Shoot();
        }
        GetWeaponInputs();
    }

    private void GetWeaponInputs()
    {
        if(myPlayer.GetButtonDown("Weapon0"))
        {
            myController.ChangeWeapon(0);
        }
        if (myPlayer.GetButtonDown("Weapon1"))
        {
            myController.ChangeWeapon(1);
        }
    }
}
