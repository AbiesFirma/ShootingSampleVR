using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] bool shooting2D = true;
    [SerializeField] float backDistance3D = 60.0f;

    [SerializeField] bool shootingVRMode = false;
    Quaternion defRot;

    [SerializeField] bool fullMode = false;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("PlayerRoot");
        }

        if(shootingVRMode)
        {
            defRot = transform.rotation;
        }
    }
        
    void Update()
    {
        if (shooting2D)
        {
            Vector3 playerPos = player.transform.position;
            this.transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);
        }
        else if(shootingVRMode)
        {
            transform.rotation = defRot;
        }
        else if(fullMode)
        {
            Vector3 playerPos = player.transform.position;
            this.transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);
        }
        else
        {
            Vector3 playerPos = player.transform.position;
            this.transform.position = new Vector3(playerPos.x - backDistance3D, transform.position.y, transform.position.z);
        }
    }
}
