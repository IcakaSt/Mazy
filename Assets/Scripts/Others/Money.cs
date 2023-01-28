using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] Text MoneyText;
    [SerializeField] float moneyToAdd = 0;
    [SerializeField] float money;

    private void Start()
    {
        //PlayerPrefs.SetFloat("Money", 500000);
        money = PlayerPrefs.GetFloat("Money");      
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = (money).ToString();

        if (money != PlayerPrefs.GetFloat("Money") && moneyToAdd == 0)
        { money = PlayerPrefs.GetFloat("Money"); }

        if (PlayerPrefs.GetFloat("MoneyToAdd") != 0)
        {
            moneyToAdd = PlayerPrefs.GetFloat("MoneyToAdd");
            Debug.Log("Money to add: " + moneyToAdd);
            PlayerPrefs.SetFloat("MoneyToAdd", 0);
            PlayerPrefs.SetFloat("Money", money+ moneyToAdd);
        }
        if (moneyToAdd + PlayerPrefs.GetFloat("Money") > money)
        {

            if (Mathf.Abs(PlayerPrefs.GetFloat("Money") + moneyToAdd - money) > 1000) { money += 1000; }
            else if (Mathf.Abs(PlayerPrefs.GetFloat("Money") + moneyToAdd - money) > 100) { money += 100; }
            else  { money += 1; }
        }
        else { moneyToAdd = 0; }
    }
}
