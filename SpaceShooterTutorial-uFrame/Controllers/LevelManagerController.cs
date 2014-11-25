using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class LevelManagerController : LevelManagerControllerBase {
    [Inject]
    public SpeedPowerUpController SpeedPowerUpController { get; set; }

    public override void InitializeLevelManager(LevelManagerViewModel levelManager) {

        levelManager.GameOver = false;

        //this can be disabled andd restarted when game is over/restarted
        //generate asteroid every x seconds
        Observable.Interval(TimeSpan.FromMilliseconds(1000)).Subscribe(x =>
        {
            var asteroid = AsteroidController.CreateAsteroid();
            asteroid.Destroy.Subscribe(_ => { levelManager.Asteroids.Remove(asteroid); });
            levelManager.Asteroids.Add(asteroid);
        });

        //we should not chage the gameover property from outside of the level manager !
        //we should make a game over computed property which will determine if the game is over
        levelManager.PlayerProperty
            .Where(player => player != null)
            .Subscribe(player => player.IsAliveProperty.Where(isAlive => !isAlive).Subscribe(_ => { levelManager.GameOver = true; }))
            .DisposeWith(levelManager);
    }

    public override void RestartLevel(LevelManagerViewModel levelManager)
    {
        if (!levelManager.Player.IsAlive)
        {         
            base.RestartLevel(levelManager);
            levelManager.GameOver = false;
            this.ExecuteCommand(levelManager.Player.Restart);   
        }
    }

    public override void ShowNotification(LevelManagerViewModel levelManager, string arg)
    {
        levelManager.NotificationText = arg;
        base.ShowNotification(levelManager, arg);
    }
}
