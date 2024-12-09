using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Sprite up_handle_sp, down_handle_sp;
    public Image handle_img;
    public Button handle_btn;
    private float resetDelay = 0.5f;
    public GameObject betHolder;
    public TextMeshProUGUI playerCoinTxt;
    public TextMeshProUGUI resultTxt;
    [HideInInspector]
    public string won = "You Won",lose="You Loss",lessCoin="Insufficent Coins";

    private void Awake()
    {
        handle_btn.onClick.AddListener(OnClickHandle);
        SetHandleSprite(up_handle_sp);
        GameManager.playerCoin = 150;
        UpdatePlayerCoins();
    }

    /// <summary>
    /// Set Game Result Text
    /// </summary>
    /// <param name="s"></param>
    internal void SetResultTxt(string s)
    {
        resultTxt.text = s;
        Invoke("ResultReset", 1f);
    }
    /// <summary>
    /// Reset Result text
    /// </summary>
    private void ResultReset()
    {
        resultTxt.text = "";
    }
    /// <summary>
    /// Update Player Coins
    /// </summary>
    public void UpdatePlayerCoins()
    {
        playerCoinTxt.text = GameManager.playerCoin.ToString();
    }

    /// <summary>
    /// On Click Bet button and x is the ammount of bet 
    /// </summary>
    /// <param name="x"></param>
    public void OnClickBet(int x)
    {
        if(GameManager.playerCoin>=x)
        {
            GameManager.betAmmount = x;
            GameManager.playerCoin = GameManager.playerCoin - GameManager.betAmmount;
            UpdatePlayerCoins();
            StartSpin();
            ToggelBetOption(false);
        }
        else
        {
            SetResultTxt(lessCoin);
        }
    }

    /// <summary>
    /// Toggle a Bet Holder Game Object pass the activeness of Game object
    /// </summary>
    /// <param name="isActive"></param>
    public void ToggelBetOption(bool isActive)
    {
        betHolder.gameObject.SetActive(isActive);
    }
    /// <summary>
    /// On click Exit Option
    /// </summary>
    public void OnClickExitBtn()
    {
        ToggelBetOption(false);
    }

    /// <summary>
    /// Reel Spinning UI logic Controller
    /// </summary>
    private void StartSpin()
    {
        GameManager.machineSpinning = true;
        SetHandleSprite(down_handle_sp);
        GameManager.Instance.MachineSpinStart();
        StartCoroutine(ResetHandleAfterDelay());
    }
    /// <summary>
    /// On Click Handle Btn
    /// </summary>
    private void OnClickHandle()
    {
        if(GameManager.machineSpinning == false)
            ToggelBetOption(true);
    }
    private IEnumerator ResetHandleAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);
        SetHandleSprite(up_handle_sp);
    }
    private void SetHandleSprite(Sprite currentSprite)
    {
        handle_img.sprite = currentSprite;
    }
}
