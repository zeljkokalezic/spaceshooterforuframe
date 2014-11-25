using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class BasicLaserView {

    public Transform spawnPoint;

    /// Invokes FireExecuted when the Fire command is executed.
    public override void FireExecuted() {
        base.FireExecuted();
    }

    /// This binding will add or remove views based on an element/viewmodel collection.
    public override ViewBase CreateProjectilesView(BaseProjectileViewModel item) {
        var laser = base.CreateProjectilesView(item);
        laser.transform.position = spawnPoint.position;
        return laser;
    }
    
    /// This binding will add or remove views based on an element/viewmodel collection.
    public override void ProjectilesAdded(ViewBase item) {
        base.ProjectilesAdded(item);
    }
    
    /// This binding will add or remove views based on an element/viewmodel collection.
    public override void ProjectilesRemoved(ViewBase item) {
        base.ProjectilesRemoved(item);
    }
}
