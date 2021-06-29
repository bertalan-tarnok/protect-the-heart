using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    public static int money;

    private void Start()
    {
        money = 0;
    }

    private void Update()
    {
        moneyText.text = money.ToString() + "$";
    }
}
