using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SingletonSO", menuName ="Singleton Scriptable Objects/SingletonSO")]
public class SingletonSO : SingletonScriptableObject<SingletonSO>
{
    [SerializeField] private float _speed = 10;

    public float Speed { get => _speed; set => _speed = value; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    protected static void FirstInitialize()
    {
        Debug.Log("SingletonSO is initialized");
    }
}
