using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

//TODO:
//Powerups (weapons change)
//Mofifiers (get faster for limited time)
//Shield - On/Off
//WeaponFireSpeed - +1
//WeaponRange ? - +1
//WeaponSpread - +1
//ShipSpeed - +1

public partial class PlayerShipView { 

    /// Subscribes to the property and is notified anytime the value changes.
    public override void IsAliveChanged(Boolean value) {
        base.IsAliveChanged(value);
        this.gameObject.transform.position = Vector3.zero;
        this.gameObject.SetActive(value);
    }
    
    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    public GameObject explosion;
    public Boundary movementBoundary;

    public override void Bind()
    {
        base.Bind();
        //each view is responsible for executing his own behaviour on collision
        this.BindComponentTriggerWith<AsteroidView>(CollisionEventType.Enter, asteroidView =>
        {
            asteroidView.ExecuteDestroy();
            this.ExecuteDestroy();
        });
    }

    /// Subscribes to the state machine property and executes a method for each state.
    public override void FireStateMachineChanged(Invert.StateMachine.State value) {
        base.FireStateMachineChanged(value);
    }
    
    public override void OnStop() {
        base.OnStop();
    }

    public override void OnFire() {
        base.OnFire();
        this.PlayerShip.Weapon.Fire.Execute(null);
    }

    protected override bool CalculateFiringCommand()
    {
        return Input.GetKey(KeyCode.Space);
    }

    protected override bool CalculateShouldFire()
    {
        return PlayerShip.FiringCommand && PlayerShip.FireTimeOutElapsed;
    }

    /// Subscribes to the state machine property and executes a method for each state.
    public override void MovementStateMachineChanged(Invert.StateMachine.State value) {
        base.MovementStateMachineChanged(value);
    }

    public override void OnIdle()
    {
        base.OnIdle();
    }

    public override void OnMove()
    {
        //this whole state machine is an overkill for this simple move but let it be for learning purpouses...
        base.OnMove();
        Observable.EveryFixedUpdate().Subscribe(x => MoveShipHandler()).DisposeWhenChanged(PlayerShip.IsMovingProperty);
            
    }

    private void MoveShipHandler()
    {
        if (this != null)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rigidbody.velocity = movement * this.PlayerShip.MovementSpeed;

            rigidbody.position = new Vector3
            (
                Mathf.Clamp(rigidbody.position.x, movementBoundary.xMin, movementBoundary.xMax),
                0.0f,
                Mathf.Clamp(rigidbody.position.z, movementBoundary.zMin, movementBoundary.zMax)
            );

            rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -this.PlayerShip.MovementSpeed/2);
        }        
    }

    protected override bool CalculateIsMoving()
    {
        //wasd pressed
        return Input.GetAxis ("Horizontal") != 0f || Input.GetAxis ("Vertical") != 0f;
    }

    //THERE ARE TWO OVERIDABLE FUNCTIONS FOR EACH COMMAND !!!!!!!
    //EXECUTE{CNAME} => Executes comand in the controller !
    //{CNAME}EXECUTED => Executed after controller command is executed !

    public override void ExecuteDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        base.ExecuteDestroy();        
    }

    ///// Invokes DestroyExecuted when the Destroy command is executed.
    //public override void DestroyExecuted()
    //{
    //    base.DestroyExecuted();
    //}
}
