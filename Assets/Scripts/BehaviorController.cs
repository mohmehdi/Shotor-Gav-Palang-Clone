using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Holds all the logic objects and executes them 
/// </summary>
public class BehaviorController : MonoBehaviour{
    // currently each puzzle element has one behavior

    [SerializeReference,SubclassSelector]
    List<IActiveLogic> _activeLogics = new List<IActiveLogic>();
    [SerializeReference,SubclassSelector]
    List<IPassiveLogic> _passiveLogics = new List<IPassiveLogic>();
    DependencyProvider _provider; // TODO: this can also cache the references

    private void OnValidate() => CheckDuplicateStackableActions();

    private void CheckDuplicateStackableActions()
    {
        HashSet<Type> duplicates = new HashSet<Type>();
        foreach (var l in _activeLogics)
        {

            if (l != null && !l.Stackable)
            {
                if (duplicates.Contains(l.GetType()))
                {
                    Debug.LogError($"Cannot have multiple of type {l.GetType()} on this object -> {name}");
                    break;
                }
                else
                    duplicates.Add(l.GetType());
            }
        }
    }

    private void Awake() => _provider = new DependencyProvider(gameObject);
    private void Start() {
        CheckDuplicateStackableActions();

        foreach (var l in _activeLogics)
            l.Setup(_provider);
        foreach (var l in _passiveLogics)
            l.Setup(_provider);
    }
    void SwitchBehavior(){ 
        
    }
    void FixedUpdate() {
        foreach (var active in _activeLogics)
        {
            active.Execute();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        foreach (var l in _activeLogics)
            l.HandleCollision();
        foreach (var l in _passiveLogics)
            l.HandleCollision();
    }
}
