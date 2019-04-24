using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerVR : MonoBehaviour
{
    Rigidbody rb;

    Vector3 _currentVelocity = Vector3.zero;   //現在の移動速度
    public Transform _centerEyeAnchor = null;


    [SerializeField] float speed = 5.0f;
    [SerializeField] float stopPower = 5.0f;

    public bool unityEditor = false;
    GameObject playerRoot;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRoot = transform.parent.gameObject;

        //RuntimePlatform ph = Application.platform;
        //Debug.Log(ph);

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            unityEditor = true;
        }
        else
        {
            unityEditor = false;
        }

    }

    void Update()
    {
        if (unityEditor)
        {
            var hol = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");

            _currentVelocity = new Vector3(0, ver * speed * 10.0f, -hol * speed * 10.0f);
        }
        else
        {

            //移動計算//////////////////////////
            //コントローラ左右両対応==タッチパッドを触っている所の座標(-1 ~ 1)取得
            Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

            //Yの+の方向のMaxが0.5ぐらい(デバイスの不具合？)なので増やす
            if (primaryTouchpad.y > 0)
            {
                primaryTouchpad.y *= 2;
            }

            //タッチパッドを触ってる場所から速度計算(右:-Z,上:Y,奥:X)       
            _currentVelocity = new Vector3(0, primaryTouchpad.y * speed * 10.0f, -primaryTouchpad.x * speed * 10.0f);
        }
    }

    private void FixedUpdate()
    {
        var rot = playerRoot.GetComponent<PlayerRootAutoMoveController>().rot;
        var dir = rot * _currentVelocity;
        rb.AddForce(dir - (rb.velocity * stopPower));
    }

}

