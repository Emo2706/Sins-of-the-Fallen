using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2Spawner : MonoBehaviour
{
    [SerializeField] Slider _timeSlider;
    [SerializeField] Player _player;
    [SerializeField] Transform _spawnPosBoss;


    // Start is called before the first frame update
    void Start()
    {
        var boss = Boss2Factory.instance.GetObjFromPool();
        boss.transform.position = _spawnPosBoss.position;
        boss.player = _player;
        boss.timeSlider = _timeSlider;
    }

    
}
