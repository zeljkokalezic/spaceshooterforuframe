using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class BasicLaserController : BasicLaserControllerBase {

    [Inject]
    public LaserBoltController LaserBoltController { get; set; }

    public override void InitializeBasicLaser(BasicLaserViewModel basicLaser) {        
    }

    public override void Fire(BaseWeaponViewModel baseWeapon)
    {
        base.Fire(baseWeapon);
        var laser = LaserBoltController.CreateLaserBolt();
        baseWeapon.Projectiles.Add(laser);
        laser.Hit.Subscribe(_ => { baseWeapon.ParentPlayerShip.AsteroidsDestroyed++; }).DisposeWith(laser);
        laser.Destroy.Subscribe(_ => { baseWeapon.Projectiles.Remove(laser); }).DisposeWith(laser);
        baseWeapon.ParentPlayerShip.IsAliveProperty.Where(isAlive => !isAlive).Subscribe(_ => { laser.Destroy.Execute(null); }).DisposeWith(baseWeapon);
    }

}
