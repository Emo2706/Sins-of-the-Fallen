using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;

    public Vector3 spawnPosition;

    Player _player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }


    public Vector3 CheckPoint()
    {
        if(spawnPosition!=Vector3.zero)
        return spawnPosition;

        return _player.gameObject.transform.position;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void SetSpawnPosition(Vector3 pos)
    {
        spawnPosition = pos;
    }
}
