using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Life : MonoBehaviour
{
    public static Life instance;

    [SerializeField] private GameObject gameLife;
    [SerializeField] private GameObject gameOverLife;

    [SerializeField] private TMP_Text LifeText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            return;
        }
    }

    public void UIHpText(int hp)
    {
        if (hp > 0)
        {
            LifeText.text = string.Format($"X {hp}");
        }
        else
        {
            gameLife.SetActive(false);
            gameOverLife.SetActive(true);
        }
    }
}
