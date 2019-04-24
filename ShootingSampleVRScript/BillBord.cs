using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBord : MonoBehaviour
{
    GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

   
    void Update()
    {
        transform.forward = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);

    }
}
