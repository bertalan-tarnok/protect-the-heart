using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{
    private Spawner leftSpawner;
    private Spawner rightSpawner;

    private TowerManager towerManager;

    private Animator animator;

    private Vector2Int side = Vector2Int.zero;

    private GameObject laser = null;

    private void Start()
    {
        animator = GetComponent<Animator>();

        leftSpawner = GameObject.Find("SpawnerLeft").GetComponent<Spawner>();
        rightSpawner = GameObject.Find("SpawnerRight").GetComponent<Spawner>();

        towerManager = FindObjectOfType<TowerManager>();

        if (transform.childCount > 0)
        {
            laser = transform.GetChild(0).gameObject;
        }

        if (laser != null)
        {
            laser.SetActive(false);
        }
    }

    private void Update()
    {
        side.x = transform.position.x > 0 ? 1 : -1;
        side.y = transform.position.y > 0 ? 1 : -1;

        if (side.y == 1)
        {
            laser.SetActive(towerManager.towerPositions.Contains(new Vector2(transform.position.x, -1)));
        }
    }
}
