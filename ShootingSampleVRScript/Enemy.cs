using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hp = 1;
    [SerializeField] ParticleSystem explodeParticlePrefab;
    [SerializeField] int point = 1;

    [SerializeField] GameObject gameMaster;
    [SerializeField] GameObject player;

    //[SerializeField] Renderer ren;
    [SerializeField] GameObject renObj;
    SkinnedMeshRenderer ren;

    GameObject deleteWall;
    bool stopGame = false;

    AudioSource audioSource;

    [SerializeField] GameObject shadowPrefab;

    [SerializeField] bool full3D = false;


    void Start()
    {
        if (gameMaster == null)
        {
            gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("PlayerRoot");
        }

        audioSource = GetComponent<AudioSource>();

        ren = renObj.GetComponent<SkinnedMeshRenderer>();

        var shadowPos = new Vector3(transform.position.x, 0.1f, transform.position.z);
        var shadow = Instantiate(shadowPrefab, transform.position, transform.rotation);
        shadow.GetComponent<ShadowController>().enemy = this.gameObject;

        deleteWall = GameObject.Find("EnemyWallBack");
    }

    
    void Update()
    {
        if(!stopGame)
        {
            stopGame = gameMaster.GetComponent<GameMaster>().stopGame;
        }

        var dis = transform.position - player.transform.position;
        var _dis = dis.magnitude;

        if (ren != null)
        { 
            if (_dis >= 150.0f && hp > 0)
            {
                ren.enabled = false;
            }
            else
            {
                ren.enabled = true;
            }
        }

        //
        if (!full3D)
        {
            if (transform.position.x <= deleteWall.transform.position.x || stopGame)
            {
                Destroy(gameObject);
            }            
        }
        else
        {
            if (stopGame)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            hp -= 1;

            if (hp <= 0)
            {
                Instantiate(explodeParticlePrefab, transform.position, transform.rotation);
                gameMaster.SendMessage("EnemyDestroy", point);
                audioSource.Play();
                Destroy(gameObject, 0.2f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hp -= 1;

            if (hp <= 0)
            {
                Instantiate(explodeParticlePrefab, transform.position, transform.rotation);
                gameMaster.SendMessage("EnemyDestroy", point);
                audioSource.Play();
                Destroy(gameObject, 0.2f);
            }
        }

    }
}
