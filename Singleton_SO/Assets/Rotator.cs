using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 direction = new Vector3(0,1,0);
    [SerializeField] float speed = 10;
    [SerializeField] SingletonSO singletonSO;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate((direction * singletonSO.Speed) * Time.deltaTime);
    }
}
