using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    // 4人分のスポーン座標
    private static readonly Vector3[] SpawnPositions = new Vector3[]
    {
        new Vector3(5, 1, 0),    // プレイヤー1
        new Vector3(-5, 1, 0),   // プレイヤー2
        new Vector3(0, 1, 5),    // プレイヤー3
        new Vector3(0, 1, -5)    // プレイヤー4
    };

    private int _playerCount = 0;

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            // プレイヤーの入場順序に基づいてスポーン座標を決定
            int spawnIndex = _playerCount % SpawnPositions.Length;
            Vector3 spawnPosition = SpawnPositions[spawnIndex];

            // 原点に向くようなクォータニオンを計算
            Quaternion lookAtRotation = Quaternion.LookRotation(Vector3.zero - spawnPosition);

            // スポーン
            Runner.Spawn(PlayerPrefab, spawnPosition, lookAtRotation);

            _playerCount++;
        }
    }
}