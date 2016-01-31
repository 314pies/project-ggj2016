using UnityEngine;
//using UnityEngine.Network;
using System.Collections;
using UnityEngine.UI;

public class MpLobby : MonoBehaviour {
    static public bool IsServer = false;
    NetworkView nView;
    void Awake()
    {
        PlayerCount = 1;
        nView = GetComponent<NetworkView>();
    }
    public void StartServer()
    {
        Network.InitializeServer(4, 25001, false);
    }

    public InputField IpInput;
    bool Lock=false;
    public void ConnectToServer()
    {   if(!Lock)
            Network.Connect(IpInput.text, 25001);
        Lock = true;
    }

    public GameObject StartMPGameButton;
    public Text ShowIP;
    void OnServerInitialized()
    {
        StartMPGameButton.SetActive(true);
        PlayerNetworkDatas[0] = Network.player;
        Debug.Log("Server Initialized");
        IsServer = true;
        ShowIP.gameObject.SetActive(true);
        ShowIP.text = "IP: " + Network.player.ipAddress;
        Names[0] = NameINput.text;
    }

    static public int PlayerCount=1;
    static public NetworkPlayer[] PlayerNetworkDatas= new NetworkPlayer[4];
    void OnPlayerConnected(NetworkPlayer player)
    {
        PlayerNetworkDatas[PlayerCount] = player;
        nView.RPC("GetMyIndex", player, PlayerCount);//Tell the player its index
        PlayerCount++;
        nView.RPC("SynPlayersNum", RPCMode.All, PlayerCount);
    }
    public InputField NameINput;
    public void StartTheGame()
    {
        if(Network.isServer)
          nView.RPC("StartMpGame", RPCMode.All);
    }
    /*Alll Remore Produce called function*/

    
    void UploadNameToServer(int Index)
    {
        nView.RPC("UploadName", RPCMode.Server, NameINput.text, Index);

    }
    static public string[] Names = new string[5];
    [RPC]
    void UploadName(string Name, int Index)
    {
        Names[Index] = Name;
        Debug.Log("Name : " + Name);
    }

    public Text ShowPlayNum;
    [RPC]
    void SynPlayersNum(int NewPlayerNum)
    {
        PlayerCount = NewPlayerNum;
        ShowPlayNum.text = "ConnectdPlayer : " + PlayerCount;
    }
    static public int MyIndex;
    [RPC]
    void GetMyIndex(int Index)
    {
        MyIndex = Index;
        IsServer = false;//Marked, may have bug
        UploadNameToServer(Index);
    }
    [RPC]
    void StartMpGame()
    {
        Application.LoadLevel(1);
    }
}
