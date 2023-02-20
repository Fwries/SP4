using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coinCounter : MonoBehaviour
{
    public TMP_Text coinText;
    int currentCoin = 0;
    // Start is called before the first frame update
    void Awake()
    {
        coinText.text = "X" + currentCoin.ToString();
    }

    public void SetCoins(int n)
    {
        currentCoin = n;
        coinText.text = "X" + currentCoin.ToString();
    }
}
