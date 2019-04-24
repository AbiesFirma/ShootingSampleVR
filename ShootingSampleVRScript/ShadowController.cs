using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {

    }

    void Update()
    {
        if (enemy == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
        }       
    }
}
