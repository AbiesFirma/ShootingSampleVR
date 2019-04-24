using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int life = 3;
    bool gameOver = false;
    GameObject gameMaster;
    AudioSource audioSource;

    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] GameObject[] lifeObj;


    void Start()
    {
        gameMaster = GameObject.Find("GameMaster");
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(life <= 0)
        {
            gameOver = true;
            gameMaster.SendMessage("GameOver");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(life == 3)
            {
                lifeObj[0].SetActive(false);
            }
            else if(life == 2)
            {
                lifeObj[1].SetActive(false);
            }
            else if (life == 1)
            {
                lifeObj[2].SetActive(false);
            }

            life -= 1;
            audioSource.Play();
            Instantiate(hitParticlePrefab, transform.position, transform.rotation);

        }
    }
}
