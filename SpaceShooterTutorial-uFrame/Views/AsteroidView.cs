using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class AsteroidView {

    private Transform _transform;

    public override void Start()
    {
        base.Start();
        _transform = transform;
    }

    /// Subscribes to the property and is notified anytime the value changes.
    public override void PowerUpChanged(PowerUpBaseViewModel value) {
        base.PowerUpChanged(value);
    }

    protected override Vector3 CalculatePosition()
    {
        return _transform.position;
    }

    //this is used instead of the implementation above (CalculatePosition)
    protected override IObservable<Vector3> GetPositionObservable()
    {
        return PositionAsObservable.Sample(TimeSpan.FromSeconds(1f));
    }

    public GameObject explosion;

    public override void Bind()
    {
        base.Bind();
        //destroy asteroid after 10 seconds
        //always dispose the binding !!!
        Observable.Interval(TimeSpan.FromMilliseconds(10000)).Subscribe(x =>
        {
            this.ExecuteDestroy(false);

        }).DisposeWith(this);

        this.BindComponentTriggerWith<BaseProjectileViewBase>(CollisionEventType.Enter, x =>
        {
            if(this.Asteroid.PowerUp != null)
            {
                this.Asteroid.PowerUp.ApplyPowerUp.Execute(this.Asteroid.ParentLevelManager.Player);
            }            
            this.ExecuteDestroy();
        }).DisposeWith(this);
    }

    /// <summary>
    /// Default argument value compiles in VS2013 but it wont work in Unity so we added this method
    /// It just calls ExecuteDestroy(true);
    /// </summary>
    public void ExecuteDestroy()
    {
        ExecuteDestroy(true);
    }

    public override void ExecuteDestroy(bool showExplosion = true)
    {
        if (showExplosion)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }        
        base.ExecuteDestroy(showExplosion);
        Destroy(this.gameObject);
    }

}
