using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxTeleport : MonoBehaviour {

    public Transform player;
    public Transform[] spawns;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            player.position = spawns[0].position + Vector3.up * 10;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            player.position = spawns[1].position + Vector3.up * 10;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            player.position = spawns[2].position + Vector3.up * 10;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            player.position = spawns[3].position + Vector3.up * 10;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            player.position = spawns[4].position + Vector3.up * 10;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            player.position = spawns[5].position + Vector3.up * 10;
    }
}
