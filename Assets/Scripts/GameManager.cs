using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool pause;

    public List<EnemyNormal> enemyNormals;

    [SerializeField] Canvas _canvasSliderFire;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (pause == true)
            _canvasSliderFire.gameObject.SetActive(false);

        else
            _canvasSliderFire.gameObject.SetActive(true);
    }
}
