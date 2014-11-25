using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class PlayerShipController : PlayerShipControllerBase {
    
    public override void InitializePlayerShip(PlayerShipViewModel playerShip) {

        playerShip.FireTimeOutElapsed = true;
        playerShip.ShouldFireProperty.Subscribe(x => ShouldFireChanged(playerShip, x));

        //NOT GOOD IDEA !
        //SEPARATION OF CONCERNS
        //LEVEL MANAGER SHOULG DETERMINE IF GAME IS OVER NOT THE PLAYER SHIP
        //playerShip.Destroy.Subscribe(_ => { playerShip.ParentLevelManager.GameOver = true; });
    }

    private void ShouldFireChanged(PlayerShipViewModel playerShip, bool value)
    {
        if (value)
        {
            playerShip.FireTimeOutElapsed = false;
            Observable.Timer(TimeSpan.FromMilliseconds(playerShip.Weapon.FireRate)).Subscribe(x =>
            {
                playerShip.FireTimeOutElapsed = true;
            });   
        }
    }

    public override void Destroy(PlayerShipViewModel playerShip)
    {
        base.Destroy(playerShip);
        playerShip.IsAlive = false;
        //it is wrong albeit posssible to call the level manager here (tight coupling)
        //instead level manager will subscribe to destroy command executed
    }

    public override void Restart(PlayerShipViewModel playerShip)
    {
        base.Restart(playerShip);
        playerShip.IsAlive = true;
        //we should trigger restart function in the weapon not set the property here
        //and ofcourse check for null
        //we could have done this for the time that it took to write this text :)
        playerShip.Weapon.FireRate = 500;
        playerShip.MovementSpeed = 5;
        playerShip.AsteroidsDestroyed = 0;
    }
}
