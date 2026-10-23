using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Conection : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        print("Conectado al master");
    }
   override
    public void OnConnectedToMaster()
    {
        print("Conectando a Master Void OnConnectedToMaster");
    }
    public void ButtonConnect()
    {
        RoomOptions options = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom("room1", options, TypedLobby.Default);
    }
    override
    public void OnJoinedRoom()
    {
        Debug.Log("Te has conectado a la sala" + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Hay " + PhotonNetwork.CurrentRoom.PlayerCount + " jugadores conectados");
    }
    private void Update()
    {
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            PhotonNetwork.LoadLevel(1);
            Debug.Log("Cargando level 1");
        }
    }
}
