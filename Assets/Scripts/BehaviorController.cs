using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Holds all the logic objects and executes them 
/// </summary>
public class BehaviorController : MonoBehaviour{
    // currently each puzzle element has one behavior

    public List<IActiveLogic> CurrActiveLogics {get;private set;}
    public List<IPassiveLogic> CurrPassiveLogics {get;private set;}

    [SerializeReference,SubclassSelector]
    List<IActiveLogic> activeLogics = new List<IActiveLogic>();
    [SerializeReference,SubclassSelector]
    List<IPassiveLogic> passiveLogics = new List<IPassiveLogic>();
    
    DependencyProvider _provider; // TODO: this can also cache the references

    private void OnValidate() => CheckDuplicateStackableActions();

    private void CheckDuplicateStackableActions()
    {
        HashSet<Type> duplicates = new HashSet<Type>();
        foreach (var l in activeLogics)
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
    private void Start()
    {
        CheckDuplicateStackableActions();
        SwitchBehavior(activeLogics, passiveLogics);

        FreezeTime.Instance.OnFreeze.AddListener(Freeze);
        FreezeTime.Instance.OnDeFreeze.AddListener(DeFreeze);

    }

    private void SetupDependencies()
    {
        foreach (var l in CurrActiveLogics)
            l.Setup(_provider);
        foreach (var l in CurrPassiveLogics)
            l.Setup(_provider);
    }

    void Freeze(){
        foreach (var l in CurrActiveLogics){
            if (l is IFreezeEffect)
                (l as IFreezeEffect).OnFreeze();
        }
        foreach (var l in CurrPassiveLogics){
            if (l is IFreezeEffect)
                (l as IFreezeEffect).OnFreeze();
        }
    }
    void DeFreeze(){
        foreach (var l in CurrActiveLogics){
            if (l is IFreezeEffect)
                (l as IFreezeEffect).OnDeFreeze();
        }
        foreach (var l in CurrPassiveLogics){
            if (l is IFreezeEffect)
                (l as IFreezeEffect).OnDeFreeze();
        }
    }
    public void SwitchBehavior(List<IActiveLogic> active,List<IPassiveLogic> passive){ 
        CurrActiveLogics = active;
        CurrPassiveLogics = passive;
        SetupDependencies();
    }
    void FixedUpdate() {
        foreach (var active in CurrActiveLogics)
            active.Execute();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        foreach (var l in CurrActiveLogics)
            l.HandleCollision(other);
        foreach (var l in CurrPassiveLogics)
            l.HandleCollision(other);
    }
}
