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
    DependencyProvider _provider = new DependencyProvider(); // TODO: this can also cache the references

    private void Start() {
        var move = new MoveLogic();
        move.Setup(_provider,gameObject);
        _activeLogicList.Add(move);
    }
    void SwitchBehavior(){ 
        
    }
    void FixedUpdate() {
        foreach (var active in _activeLogicList)
        {
            active.Execute();
        }
    }
}
