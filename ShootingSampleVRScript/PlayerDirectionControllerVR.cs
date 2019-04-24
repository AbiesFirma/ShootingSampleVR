using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirectionControllerVR : MonoBehaviour
{
    Rigidbody rb;

    GameObject centerEye;
    GameObject playerRoot;

    //[SerializeField] GameObject player;

    Quaternion defRot;
    float defDis;
    float currentDis;
    [SerializeField] float revercePower = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        centerEye = GameObject.FindGameObjectWithTag("MainCamera");
        playerRoot = transform.parent.gameObject;
        
        defRot = transform.rotation;
        defDis = (transform.position - centerEye.transform.position).magnitude;
        Debug.Log(defRot);

        var rot = playerRoot.GetComponent<PlayerRootAutoMoveController>().rot;
        transform.rotation = defRot * rot;
    }


    void Update()
    {
        var rot = playerRoot.GetComponent<PlayerRootAutoMoveController>().rot;
        transform.rotation = defRot * rot;


        var dis = transform.position - centerEye.transform.position;
        currentDis = dis.magnitude - defDis;
        Debug.Log(currentDis);

        if (currentDis > 0.1f || currentDis < -0.1f)
        {
            rb.AddForce(dis.normalized * currentDis * -revercePower);
        }
        
    }
}
