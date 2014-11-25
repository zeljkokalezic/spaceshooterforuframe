using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class LevelManagerView {


    public GUIText InfoText;
    public GUIText ScoreText;
    public GUIText GameOverText;
    public GUIText RestartText;

    /// Subscribes to the property and is notified anytime the value changes.
    public override void NotificationTextChanged(String value) {
        base.NotificationTextChanged(value);
        InfoText.text = value;
        Observable.Timer(TimeSpan.FromMilliseconds(500)).Subscribe(x =>
        {
            InfoText.text = string.Empty;
        });   
    }

    /// Subscribes to the property and is notified anytime the value changes.
    public override void PlayerChanged(PlayerShipViewModel ship)
    {
        base.PlayerChanged(ship);
    }

    /// Subscribes to the property and is notified anytime the value changes.
    public override void GameOverChanged(Boolean value)
    {
        base.GameOverChanged(value);
        GameOverText.enabled = RestartText.enabled = value;
    }

    /// Invokes RestartLevelExecuted when the RestartLevel command is executed.
    public override void RestartLevelExecuted()
    {
        base.RestartLevelExecuted();
    }

    /// Subscribes to the property and is notified anytime the value changes.
    public override void SpawnPointChanged(Vector3 value)
    {
        base.SpawnPointChanged(value);
    }

    /// This binding will add or remove views based on an element/viewmodel collection.
    public override ViewBase CreateAsteroidsView(AsteroidViewModel item)
    {
        var prefabName = string.Format("{0}{1}", "Asteroid", UnityEngine.Random.Range(1,6));
        var ast = InstantiateView(prefabName, item);
        ast.InitializeData(ast.ViewModelObject);
        ast.transform.position = new Vector3(UnityEngine.Random.Range(-this.LevelManager.SpawnPoint.x, this.LevelManager.SpawnPoint.x), this.LevelManager.SpawnPoint.y, this.LevelManager.SpawnPoint.z);
        return ast;
    }

    public override void Bind()
    {
        base.Bind();
        UpdateAsObservable().Where(_ => Input.GetKey(KeyCode.R)).Subscribe(_ => { this.ExecuteRestartLevel(); });
        //this translates to above, we are just showing diferent ,methods of doing things ...
        //NOTE: this requires that we setup input button restart in Edit -> Project Settings -> Input
        //this.BindInputButton(LevelManager.RestartLevel, "Restart", InputButtonEventType.ButtonDown);
    }

    /// This binding will add or remove views based on an element/viewmodel collection.
    public override void AsteroidsAdded(ViewBase item)
    {
        base.AsteroidsAdded(item);
    }

    /// This binding will add or remove views based on an element/viewmodel collection.
    public override void AsteroidsRemoved(ViewBase item)
    {
        base.AsteroidsRemoved(item);
    }

    /// Subscribes to the property and is notified anytime the value changes.
    public override void ScoreChanged(Int32 value)
    {
        base.ScoreChanged(value);
        ScoreText.text = string.Format("Score: {0}", value);
    }
}
