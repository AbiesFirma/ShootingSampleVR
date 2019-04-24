using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPointerController : MonoBehaviour
{
    LineRenderer line;
    GameObject gameMaster;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        gameMaster = GameObject.Find("GameMaster");
    }

    void Update()
    {
        var gameOver = gameMaster.GetComponent<GameMaster>().gameOver;
        var gameClear = gameMaster.GetComponent<GameMaster>().gameClear;

        if(gameOver || gameClear)
        {
            line.startWidth = 0.1f;
            line.endWidth = 0.05f;
        }
        else
        {
            line.startWidth = 0.0f;
            line.endWidth = 0.0f;
        }
    }
}
