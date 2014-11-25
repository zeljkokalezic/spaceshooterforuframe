using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class LaserBoltView { 

    /// Invokes DestroyExecuted when the Destroy command is executed.
    public override void DestroyExecuted() {
        base.DestroyExecuted();
        Destroy(this.gameObject);
    }


    public override void Bind()
    {
        base.Bind();

        this.BindComponentTriggerWith<AsteroidView>(CollisionEventType.Enter, asteroidView =>
        {
            //asteroid should be responsible for destroying himself
            //this creates null exceptions later becouse it executes in the same time as the binding from the asteroid view
            //and creates a race condition where we get null exception for parent level manager in asteroid, becouse we have 
            //a subscription to destroy command from level manager where the asteroid is removed from collection
            //asteroidView.ExecuteDestroy();
            this.ExecuteHit();
            this.ExecuteDestroy();
        }).DisposeWith(this);

        Observable.Interval(TimeSpan.FromMilliseconds(10000)).Subscribe(x =>
        {
            if (this != null)
            {
                this.ExecuteDestroy();
            }
        }).DisposeWith(this);

        rigidbody.velocity = transform.forward * 10;
    }

    public override void ExecuteDestroy()
    {
        base.ExecuteDestroy();
        Destroy(this.gameObject);
    }
}
