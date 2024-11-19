using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpSystem : MonoBehaviour
{
    private Slider hpBar;

    private void Start()
    {
        hpBar = GetComponent<Slider>();
        hpBar.gameObject.SetActive(false);
    }

    private void Update()
    {
    }
}
