using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Holds all the logic objects and executes them 
/// </summary>
public class BehaviorController : MonoBehaviour
{
    // currently each puzzle element has one behavior

    public List<IActiveLogic> CurrActiveLogics { get; private set; }
    public List<IPassiveLogic> CurrPassiveLogics { get; private set; }

    [SerializeReference, SubclassSelector]
    List<IActiveLogic> activeLogics = new List<IActiveLogic>();
    [SerializeReference, SubclassSelector]
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

    private void Restart()
    {
        gameObject.SetActive(true);
        SwitchBehavior(activeLogics, passiveLogics);
        foreach (var l in CurrActiveLogics)
            l.Reset();
        foreach (var l in CurrPassiveLogics)
            l.Reset();
    }
    private void Awake() => _provider = new DependencyProvider(gameObject);

    private void Start()
    {
        GameManager.Instance.OnRestart.AddListener(Restart);
        GameManager.Instance.OnFreeze.AddListener(Freeze);
        GameManager.Instance.OnDeFreeze.AddListener(DeFreeze);
        CheckDuplicateStackableActions();
        SwitchBehavior(activeLogics, passiveLogics);

        foreach (var l in CurrActiveLogics)
            l.Start();
        foreach (var l in CurrPassiveLogics)
            l.Start();
    }

    private void SetupDependencies()
    {
        foreach (var l in CurrActiveLogics)
            l.Setup(_provider);
        foreach (var l in CurrPassiveLogics)
            l.Setup(_provider);
    }

    void Freeze()
    {
        if (!gameObject.activeSelf)
            return;
            
        foreach (var l in CurrActiveLogics)
        {
            if (l is IFreezeEffect)
                (l as IFreezeEffect).OnFreeze();
        }
        foreach (var l in CurrPassiveLogics)
        {
            if (l is IFreezeEffect)
                (l as IFreezeEffect).OnFreeze();
        }
    }
    void DeFreeze()
    {
        if (!gameObject.activeSelf)
            return;

        foreach (var l in CurrActiveLogics)
        {
            if (l is IFreezeEffect)
                (l as IFreezeEffect).OnDeFreeze();
        }
        foreach (var l in CurrPassiveLogics)
        {
            if (l is IFreezeEffect)
                (l as IFreezeEffect).OnDeFreeze();
        }
    }
    public void SwitchBehavior(List<IActiveLogic> active, List<IPassiveLogic> passive)
    {
        CurrActiveLogics = active;
        CurrPassiveLogics = passive;
        SetupDependencies();

        foreach (var pass in CurrPassiveLogics)
            pass.Execute();
    }
    void FixedUpdate()
    {
        foreach (var active in CurrActiveLogics)
            active.Execute();
    }
    private void OnCollisionEnter2D(Collision2D other)
    { // TODO: Switch two Kinematic bodies with trigger, this is buggy
        foreach (var l in CurrActiveLogics)
            l.HandleCollision(other);
        foreach (var l in CurrPassiveLogics)
            l.HandleCollision(other);
    }
}
