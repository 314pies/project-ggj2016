using UnityEngine;
using System.Collections;

public class NetworkControllerInGame : MonoBehaviour
{
    public CompositionRoot compositionroot;
    static public bool LockLocalMovement;

    NetworkView nView;
    void Awake()
    {
        nView = GetComponent<NetworkView>();
    }
    public GameObject Master;

    public GameObject[] AllAI = new GameObject[30];
    public CharacterView[] AllAIChaV = new CharacterView[30];
    public Character[] AllAICha = new Character[30];


    public GameObject[] OtherPlayersRemote = new GameObject[5];
    public CharacterView[] OtherPlayersAIChaV = new CharacterView[5];
    public Character[] OtherPlayersCha = new Character[5];

    public GameObject LocalPlayer;
    public CharacterView LocalPlayerChaV;
    public Character LocalPlayerCha;

    int LastAIIndex = 0;
    public Character[] CharactersController = new Character[30];

    public GameManager gameManager;
    
    // Update is called once per frame



    void Update()
    {
        if (Network.isServer)
        {
            for (int i = 0; i < 30; i++)
            {
                if (AllAI[i] != null)
                    nView.RPC("UpdateAITrans", RPCMode.Others, i, AllAI[i].transform.position, AllAI[i].transform.localScale);
            }
            //  UpdateMasterTrans(Master.transform.position, Master.transform.localScale);
            nView.RPC("UpdateMasterTrans", RPCMode.Others, Master.transform.position, Master.transform.localScale);
        }

        if (!LockLocalMovement)
            nView.RPC("UpdateRtPlayerTrans", RPCMode.Others, MpLobby.MyIndex, LocalPlayer.transform.position, LocalPlayer.transform.localScale, LocalPlayerCha.IsDoingAction());

    }


    public void PushAI(int AI_Index, float dirX, float dirY)
    {
        nView.RPC("PushAIRemote", RPCMode.All, AI_Index, dirX, dirY);
    }

    public void PushPlayer(int PlayerIndex, float dirX, float dirY)
    {
        Debug.Log("Player is pushed: " + PlayerIndex);
        nView.RPC("PushPlayerRemote", RPCMode.All, PlayerIndex, dirX, dirY);
    }
    public void SynPushAnim()
    {
        nView.RPC("SynPlayerPush", RPCMode.Others, MpLobby.MyIndex);
    }

    public void KillAnAI(int AIIndex)
    {
        nView.RPC("KillAnAIRemote", RPCMode.Others, AIIndex);

        //Killed an AI;
    }

    public void KillAnPlayer(int RPIndex)
    {
        nView.RPC("KillAnPlayerRemote", RPCMode.Others, RPIndex);
    }



    public void AllStartTheGame()
    {
        nView.RPC("StartTheRound", RPCMode.All);
    }
    //public void Syncro
    //Create myself remote



    [RPC]
    void StartTheRound()
    {
        gameManager.StartTheGame();
    }

    [RPC]
    void KillAnPlayerRemote(int RemotePlayerIndex)
    {
        //AllAICha[RemotePlayerIndex].Kill();
        if (RemotePlayerIndex != MpLobby.MyIndex)
            OtherPlayersCha[RemotePlayerIndex].Kill();
        else 
            LocalPlayerCha.Kill();
    }

    [RPC]
    void KillAnAIRemote(int AIIndex)
    {
        AllAICha[AIIndex].Kill();
    }

    [RPC]
    void UpdateAITrans(int ObjIndex, Vector3 NewPos, Vector3 NewScale)
    {

        if (AllAI[ObjIndex] != null)
        {
            AllAI[ObjIndex].transform.position = NewPos;
            AllAI[ObjIndex].transform.localScale = NewScale;
        }
        else
        {
            Debug.LogWarning("An AI isn't be signed ,Index : " + ObjIndex);
        }
    }

    [RPC]
    void UpdateRtPlayerTrans(int ObjIndex, Vector3 NewPos, Vector3 NewScale, bool IsAction)
    {
        if (OtherPlayersRemote[ObjIndex] != null)
        {
            OtherPlayersRemote[ObjIndex].transform.position = NewPos;
            OtherPlayersRemote[ObjIndex].transform.localScale = NewScale;
            OtherPlayersCha[ObjIndex].DoAction(IsAction);
        }
    }

    [RPC]
    void SynPlayerPush(int PlayerIndex)
    {
        if (OtherPlayersCha[PlayerIndex] != null)
            OtherPlayersCha[PlayerIndex].Push();
        else
            Debug.Log("Error! Player ID" + PlayerIndex + " not exist");
    }

    [RPC]
    void UpdateMasterTrans(Vector3 LatestPos, Vector3 LocalScale)
    {
        if (Master != null)
        {
            Master.transform.position = LatestPos;
            Master.transform.localScale = LocalScale;
        }
    }

    [RPC]
    void PushAIRemote(int AIIndex, float dirX, float dirY)
    {
        Vector2 NewDir = Vector2.zero;
        NewDir.x = dirX;
        NewDir.y = dirY;
        AllAICha[AIIndex].Fall(NewDir);
    }

    [RPC]
    void PushPlayerRemote(int PlayerIndex, float dirX, float dirY)
    {
        Debug.Log("Got Pushed message");
        Vector2 NewDir = Vector2.zero;
        NewDir.x = dirX;
        NewDir.y = dirY;
        if (PlayerIndex != MpLobby.MyIndex)
        {
            OtherPlayersCha[PlayerIndex].Fall(NewDir);
        }
        else
        {
            LockLocalMovement = true;
            LocalPlayerCha.Fall(NewDir);
            StartCoroutine(UnlockAfter());
        }
    }

    IEnumerator UnlockAfter()
    {
        yield return new WaitForSeconds(0.5f);
        LockLocalMovement = false;
    }
    

}
