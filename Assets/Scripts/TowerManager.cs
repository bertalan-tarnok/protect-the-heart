using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    [SerializeField] private GameObject laser;

    public List<Vector2> towerPositions = new List<Vector2>();
    public List<GameObject> towers = new List<GameObject>();

    private int cost = 4;

    private void Start()
    {
        AddTower(new Vector2(6, 1), true);
        AddTower(new Vector2(6, -1), true);

        AddTower(new Vector2(-6, 1), true);
        AddTower(new Vector2(-6, -1), true);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed &&
            Select.valid &&
            !towerPositions.Contains(Select.selection) &&
            Money.money >= cost)
        {
            AddTower(Select.selection);
        }

        if (Mouse.current.rightButton.isPressed && Select.valid && towerPositions.Contains(Select.selection))
        {
            RemoveTower(Select.selection);
        }
    }

    private void AddTower(Vector2 pos, bool initial = false)
    {
        towerPositions.Add(pos);

        GameObject newTower = Instantiate(tower);

        newTower.transform.parent = transform;
        newTower.transform.position = pos;


        if (pos.y > 0)
        {
            GameObject newLaser = Instantiate(laser);
            newLaser.transform.parent = newTower.transform;
            newLaser.transform.position = new Vector2(newTower.transform.position.x, newLaser.transform.position.y);
            newTower.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }

        towers.Add(newTower);

        if (!initial) Money.money -= cost;
    }

    private void RemoveTower(Vector2 pos)
    {
        int index = towerPositions.IndexOf(pos);

        Destroy(towers[index]);

        towers.RemoveAt(index);
        towerPositions.RemoveAt(index);

        Money.money += cost;
    }
}
