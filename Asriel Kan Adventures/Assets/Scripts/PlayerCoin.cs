using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCoin : MonoBehaviour
{

    public int coins = 0;
    public TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddCoin(int suma)
    {
        coins = coins + suma;

        coinsText.text = coins.ToString();

        Debug.Log("Player got a coins. His current amount of coins is " + coins);
    }
}
