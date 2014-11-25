using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Invert.StateMachine;


public class FireStateMachine : FireStateMachineBase {
    
    public FireStateMachine(ViewModel vm, string propertyName) : 
            base(vm, propertyName) {
    }
}
