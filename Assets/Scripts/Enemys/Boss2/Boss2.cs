using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2 : EnemyGlobalScript
{
    Boss2View _view;
    public Slider timeSlider;
    public Slider hpBar;
    [SerializeField] Transform _spawnPositionBall;
    bool _fightStarted;
    public Player player;

    private void Awake()
    {
        _view = new Boss2View();
    }

    protected override void Start()
    {
        base.Start();
        ColliderSecondLevel.StartBoss += StartFight;
    }

    public static void TurnOnCallBack(Boss2 boss)
    {
        boss.gameObject.SetActive(true);
    }

    public static void TurnOffCallBack(Boss2 boss)
    {
        boss.gameObject.SetActive(false);
    }


    void StartFight()
    {
        _fightStarted = true;

        var ball = Boss2BallFactory.instance.GetObjFromPool();
        ball.transform.position = _spawnPositionBall.position;
        ball.timeSlider = timeSlider;
        ball.hpBar = hpBar;
        //Agregar efecto y sonido
    }

    private void OnDestroy()
    {
        ColliderSecondLevel.StartBoss -= StartFight;
    }
}
