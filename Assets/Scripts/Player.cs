using UnityEngine;
using Photon.Pun;

// CODIGO CORREGIDO
public class Player : MonoBehaviour
{
    public float speed, jumpForce;
    private Rigidbody2D rig;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Esto viene de la librería Photon.Pun
       if (GetComponent<PhotonView>().IsMine)
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.position = transform.position + (Vector3.up) + transform.forward * -10;
            //Debug.Log("Este es mi jugador");
            rig.linearVelocity = (transform.right * speed * Input.GetAxis("Horizontal")) +
            (transform.up * rig.linearVelocity.y);

            if (rig.linearVelocity.x > 0.1f)
                GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, false);
            // quitado en el video 9 revisar
            //GetComponent<SpriteRenderer>().flipX = false;
            //RotateSprite(false);
            else if (rig.linearVelocity.x < -0.1f)
                GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, true);

            // quitado en el video 9 revisar
            //GetComponent<SpriteRenderer>().flipX = true;
            //RotateSprite(true);

                if (Input.GetButtonDown("Jump"))
            {
                rig.AddForce(transform.up * jumpForce);
            }
            anim.SetFloat("velocityX", Mathf.Abs(rig.linearVelocity.x));
            anim.SetFloat("velocityY", rig.linearVelocity.y);
        }
       
    }
    [PunRPC]
    public void RotateSprite (bool rotate)
    {
        GetComponent<SpriteRenderer>().flipX = rotate;
    }
    
}
