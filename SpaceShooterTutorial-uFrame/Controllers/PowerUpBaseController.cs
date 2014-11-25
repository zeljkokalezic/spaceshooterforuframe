using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class PowerUpBaseController : PowerUpBaseControllerBase {
    
    public override void InitializePowerUpBase(PowerUpBaseViewModel powerUpBase) {
    }

    public override void ApplyPowerUp(PowerUpBaseViewModel powerUpBase, PlayerShipViewModel arg)
    {
        base.ApplyPowerUp(powerUpBase, arg);
        //globally registered instance !
        LevelManager.ShowNotification.Execute(powerUpBase.Description);
    }
}
