using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Conection : MonoBehaviourPunCallbacks
{
    private bool escenaSolicitada = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        print("Conectando al master");
    }
   override
    public void OnConnectedToMaster()
    {
        print("Conectado a Master Void OnConnectedToMaster");
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
        //Esto es para poder comprobar con el master que entra al juego
        //ComprobarJugadores();

    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("Ha entrado otro jugador. Total: " +
                   PhotonNetwork.CurrentRoom.PlayerCount);

        ComprobarJugadores();
    }
    private void ComprobarJugadores()
    {
        // Comprobar que estamos dentro de una sala.
        if (!PhotonNetwork.InRoom)
            return;

        // Solo el Master Client puede cargar la escena.
        if (!PhotonNetwork.IsMasterClient)
            return;

        // Esperar a que haya al menos dos jugadores. Aquí ponerlo a uno si queremos comprobar en el master
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
            return;

        // Evitar solicitar la carga más de una vez.
        if (escenaSolicitada)
            return;

        escenaSolicitada = true;

        Debug.Log("Cargando la escena 1 para ambos jugadores");

        PhotonNetwork.LoadLevel(1);
    }
    private void Update()
    {
        /*if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 0)
        {
            //PhotonNetwork.LoadLevel(1);
            //Esto se ejecutaría todo el rato así que lo quito, Debug.Log("Cargando level 1");
        }*/
    }
}
