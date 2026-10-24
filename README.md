
TAREA 2 UNITY PUN FREE 2D MULTIPLAYER
Creación de tarea basada en esta lista de videos de Youtube https://www.youtube.com/playlist?list=PLaf-LA5UbzJxldmANkX51PveYzx5RnVKu Ahora en Unity 6 hay algunas diferencias de código que estoy documentando.

PARREL SYNC Para pruebas multiplayer

INSTALACIÓN
Primero instalar multiplay mode
https://docs.unity3d.com/Packages/com.unity.template.mr-multiplayer@1.0/manual/test-multiplayer.html
https://github.com/VeriorPies/ParrelSync.git 

CODIGO EJEMPLO PARA COMPROBAR CUANTOS JUGADORES HAY Y EL NOMBRE DE LA SALA CREADA
SINCRONIZADO GITHUB
https://github.com/jconex/T2Multiplayer2d

using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Conection : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        print("Comenzando función Start");
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

}

ESTE SERÍA EL CÓDIGO CORREGIDO PARA VERSIÓN ANTERIOR YA QUE LUEGO VEREMOS UN PROBLEMA DE CONFIGURACIÓN QUE HAY QUE ARREGLAR O ARREGLAR ESTE CÓDIGO
using UnityEngine;

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
        rig.linearVelocity = (transform.right * speed * Input.GetAxis("Horizontal")) +
            (transform.up * rig.linearVelocity.y);

        if (rig.linearVelocity.x < 0.1f)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (rig.linearVelocity.x < -0.1f)
            GetComponent<SpriteRenderer>().flipX = true;

        if (Input.GetButtonDown("Jump"))
        {
            rig.AddForce(transform.up * jumpForce);
        }
        anim.SetFloat("velocityX", Mathf.Abs(rig.linearVelocity.x));
        anim.SetFloat("velocityY", rig.linearVelocity.y);
    }
}


1.- Creamos nueva escena y la guardamos
2.- Creamos los objetos que no queremos que se sincronicen
Bajamos assets gratuitous
pixel adventure
En la web buscar free assets
https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360

Lo añadimos a My assets

Luego importamos el paquete de Unity Registry 
2d Sprite
2d TimeMap Editor

Que ya aparecen importados no se como no aparece para instalar. Pero si se le da a locate los localiza en los paquetes ya importados

Añadimos y recortamos los sprites con un Terrain plain

Este código falla por estar obsoleto

rig.velocity = (transform.right * speed * Input.GetAxis("Horizontal")) +
     transform.up * rig.velocity.y);

 if (rig.velocity.x < 0.1f)
     GetComponent<SpriteRenderer>().flipX = false;
 else if (rig.velocity.x < 0.1f)
     GetComponent<SpriteRenderer>().flipY = false;

 if (Input.GetButtonDown("Jump"))
 {
     rig.AddForce(transform.up * jumpForce);
     Debug.Log("estoy aqui");
 }

Posible error para mantener el código que pone en el video, bucar Active Input Handling
CON ESTO QUEDA RESUELTO Y YA FUNCIONAN LOS CURSORES

Cambiar a both

La opción recomendada podría ser cambiar el código y usar el nuevo sistema de Unity 6



REALIZAMOS CLONACIÓN Y CREAMOS EL SEGUNDO PERSONAJE


REMOMBRAMOS EL OBJETO FROG A VIRTUAL
COPIAMOS EL CONTROLER Y LO PONEMOS EN LA CARPETA

SELECCIONAMOS EL OBJETO EN LA JERARQUÍA Y ARRASTRAMOS EL ANIMATION CONTROLLER

UNA VEZ HECHO ESTO, ABRIMOS EL ANIMATOR CONTROLLER Y SUSTITUIMOS TODOS LOS EVENTOS QUE TAMBIÉN HEMOS COPIADO DE FROG Y LOS HEMOS LLEVADO A LA CARPETA…


ARRASTRAMOS CADA UNO EN SU LUGAR



UNA VEZ HECHO ESTO CAMBIAMOS TODOS LAS ESCENAS DE LA ANIMACIÓN CON LOS SPRITE CORRESPONDIENTES



Y AL TERMINAR COPIAMOS TAMBIÉN EL OBJETO REALIZADO A LOS ASSETS


UNA VEZ HECHO ESTO YA PODEMOS PONER FROG EN LA ESCENA


YA ESTARÍAN LOS DOS PERSONAJES COMPLETAMENTE MONTADOS Y FUNCIONANDO CON LAS MISMAS TECLAS…



COMENZAMOS CON EL MULTIPLAYER VIDEO 6


1.- AÑADIMOS LOS COMPONENTES SIGUIENTES A LOS OBJETOS DE LOS JUGADORES



Y CAMBIAMOS EL ANIMETOR VIEW A DISCRETE A LOS DOS OBJETOS


ACTIVAMOS TELEPORT 3 METROS


APLICAMOS LOS CAMBIOS A CADA UNO  Y LOS QUITAMOS DEL GAMESCENE


CREAMOS OBJETO VACÍO Y LO LLAMAMOS SCENEMANAGER
CREAMOS CARPETA DE SCRIPTS Y MOVEMOS LOS SCRIPTS C# AHÍ


CREAMOS UN GAMEMANAGER DENTRO DE SCRIPTS Y NOS VAMOS AL CÓDIGO
IMPORTANTE AÑADIR EL SCRIPT GAMEMANAGER AL SCENEMANAGER PARA QUE CONTROLE TODA LA ESCENA SI NO NO CARGARÁ LOS OBJETOS CORRECTAMENTE Y LOS INICIARÁ


Debería cambiar a una imagen de tuerca tal que así, pero no se porque no me lo hace


Editamos el código

importamos la librería photon.pun
Añadimos quien es el master y si no el otro

Luego le añadimos donde queremos que aparezcan los jugadores
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
            PhotonNetwork.Instantiate("Frog", new Vector3(-1,-4,0), Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Virtualguy", new Vector3(-2,-4,0),Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

Creamos carpeta Resources donde pondremos lo que queramos instanciar



IMPORTANTE DA UN ERROR DE LA BUILDESENE Y HAY QUE AÑADIR TODAS LAS ESCENAS ASÍ


EN EL VIDEO INDICA BUILD SETTINGS PERO AHORA ESTÁ EN BUILD PROFILES/SCENE LIST
AHÍ AÑADIMOS LAS ESCENAS ABIERTAS



ERROR AL USAR LOS OBJETOS PARA EL PUN
Frog no le aparecían los tres componentes

PARTE 8 QUIEN ES EL PROPIETARIO DE CADA OBJETO

CON ESTE CÓDIGO YA FUNCIONA CON MASTER Y CLIENTE
AQUÍ LOS TRES SCRIPTS NECESARIOS
PLAYER, CONECTION Y GAMEMANAGER (Este último importante añadirlo al GameScene creado vacío para que actue

PLAYER.CS

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
        {   Debug.Log("Este es mi jugador");
            rig.linearVelocity = (transform.right * speed * Input.GetAxis("Horizontal")) +
            (transform.up * rig.linearVelocity.y);

            if (rig.linearVelocity.x < 0.1f)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (rig.linearVelocity.x < -0.1f)
                GetComponent<SpriteRenderer>().flipX = true;

            if (Input.GetButtonDown("Jump"))
            {
                rig.AddForce(transform.up * jumpForce);
            }
            anim.SetFloat("velocityX", Mathf.Abs(rig.linearVelocity.x));
            anim.SetFloat("velocityY", rig.linearVelocity.y);
        }
       
    }
    
}


CONNECTION.CS 

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
        //base.OnPlayerEnteredRoom(newPlayer);
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


GAMEMANAGER.CS

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



