using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaShooter : MonoBehaviour
{
    [SerializeField] GameObject bullet1Prefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootInterval = 0.5f;

    float shootTime;
    bool buttonDown;

    [SerializeField] bool vrMode = false;
    [SerializeField] GameObject centerEye;


    void Start()
    {
        shootTime = 0.0f;
        buttonDown = false;

    }

    void Update()
    {
        shootTime += Time.deltaTime;

        if (vrMode)
        {
            var editor = GetComponent<PlayerControllerVR>().unityEditor;

            if (editor)
            {
                if (Input.GetMouseButton(0))
                {
                    CharaShoot1();
                }
            }
            else
            {
                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    CharaShoot1();
                }
            }
        }
        else
        {
            if (buttonDown)
            {
                CharaShoot1();
            }
        }
        
    }

    public void CharaShoot1()
    {
        if (shootTime >= shootInterval)
        {
            GameObject bullet1 = Instantiate(bullet1Prefab, shootPoint.position, centerEye.transform.rotation);
            shootTime = 0.0f;
        }
    }

    public void rightButtonDown()
    {
        buttonDown = true;
    }
    public void rightButtonUp()
    {
        buttonDown = false;
    }
}
