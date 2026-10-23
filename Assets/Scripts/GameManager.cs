using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            // Este texto de Frog es simplemente por poner cualquiera para el primero que entre en la sala
            PhotonNetwork.Instantiate("Frog", new Vector3(2,1,0), Quaternion.identity);
            
        }
        else
        {
            PhotonNetwork.Instantiate("VirtualGuy", new Vector3(3,1,0),Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
