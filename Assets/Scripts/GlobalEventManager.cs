using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : SingletonBase<GlobalEventManager>
{
    public UnityEvent AstonautOnPicked;
    public UnityEvent AstonautOnDeath;

}

