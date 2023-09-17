using Cinemachine;
using IndividualGames.UniPoly.Multiplayer;
using IndividualGames.UniPoly.Player;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] m_spawnLocations;
    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private CinemachineVirtualCamera m_overworldCamera;

    private void Awake()
    {
        PhotonController.JoinedRoom.Connect(SpawnPlayer);
    }

    public void SpawnPlayer()
    {
        Transform location = m_spawnLocations[Random.Range(0, m_spawnLocations.Length)];

        var player = PhotonNetwork.Instantiate(m_playerPrefab.name, location.position, location.rotation);
        player.GetComponent<PlayerController>().Init();

        m_overworldCamera.Follow = player.transform;
    }
}
