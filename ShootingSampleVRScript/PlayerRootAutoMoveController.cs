using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRootAutoMoveController : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;

    GameObject gameMaster;
    bool stopGame;

    [SerializeField] bool fullMove = false;
    [SerializeField] GameObject centerEye;
    Quaternion defAngle;
    public Quaternion rot { get; private set;}

    AudioSource audioSource;
    

    void Start()
    {
        gameMaster = GameObject.Find("GameMaster");
        stopGame = false;
        audioSource = GetComponent<AudioSource>();

        defAngle = Quaternion.Euler(0, 90, 0);
        //Quaternionの差分計算
        rot = centerEye.transform.rotation * Quaternion.Inverse(defAngle);
    }

    void Update()
    {
        if (!stopGame)
        {
            stopGame = gameMaster.GetComponent<GameMaster>().stopGame;
        }
                
        AutoMove();
    }

    void AutoMove()
    {
        if (stopGame)
        {
            if (speed >= 0.001f)
            {
                speed *= 0.9f;
                audioSource.volume -= 0.01f;
                audioSource.pitch -= 0.01f;
            }
            else
            {
                speed = 0.0f;
                audioSource.volume = 0.0f;
                audioSource.pitch = 0.0f;
            }
        }

        if (!fullMove)
        {
            transform.Translate(0.1f * speed, 0, 0);
        }
        else
        {
            var forwordMove = new Vector3(0.1f* speed, 0, 0);
            //Quaternionの差分計算
            rot = centerEye.transform.rotation * Quaternion.Inverse(defAngle);
            var dir = rot * forwordMove;
            transform.Translate(dir);

        }
    }
}
