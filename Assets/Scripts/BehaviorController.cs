using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the logic objects and executes them 
/// </summary>
public class BehaviorController : MonoBehaviour{
    // currently each puzzle element has one behavior
    List<IActiveLogic> _activeLogicList = new List<IActiveLogic>();
    List<IPassiveLogic> _passiveLogicList = new List<IPassiveLogic>();

    void SwitchBehavior(){ 
        
    }
    void FixedUpdate() {
        
    }
}
