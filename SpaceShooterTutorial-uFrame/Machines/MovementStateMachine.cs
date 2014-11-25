using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Invert.StateMachine;


public class MovementStateMachine : MovementStateMachineBase {
    
    public MovementStateMachine(ViewModel vm, string propertyName) : 
            base(vm, propertyName) {
    }
}
