using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class FireRatePowerUpController : FireRatePowerUpControllerBase {
    
    public override void InitializeFireRatePowerUp(FireRatePowerUpViewModel fireRatePowerUp) {
    }

    public override void ApplyPowerUp(PowerUpBaseViewModel powerUpBase, PlayerShipViewModel arg)
    {
        base.ApplyPowerUp(powerUpBase, arg);
        if (arg.Weapon != null)
        {
            arg.Weapon.FireRate += powerUpBase.Modifier;
        }
    }
}
