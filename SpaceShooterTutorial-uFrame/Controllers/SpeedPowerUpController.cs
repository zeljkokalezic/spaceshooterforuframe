using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class SpeedPowerUpController : SpeedPowerUpControllerBase {
    
    public override void InitializeSpeedPowerUp(SpeedPowerUpViewModel speedPowerUp) {        
    }

    public override void ApplyPowerUp(PowerUpBaseViewModel powerUpBase, PlayerShipViewModel arg)
    {
        base.ApplyPowerUp(powerUpBase, arg);
        arg.MovementSpeed+= powerUpBase.Modifier;
    }
}
