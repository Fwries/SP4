using TMPro;

using Photon.Pun;

public class LoadGame : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    void Start() { PhotonNetwork.ConnectUsingSettings(); }

    public override void OnConnectedToMaster() { PhotonNetwork.JoinLobby(); }

    public void CreateRoom() { PhotonNetwork.CreateRoom(createInput.text); }

    public void JoinRoom() { PhotonNetwork.JoinRoom(joinInput.text); }

    public override void OnJoinedRoom() { PhotonNetwork.LoadLevel("SampleScene"); }
}
