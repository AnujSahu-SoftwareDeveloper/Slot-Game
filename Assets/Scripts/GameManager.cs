using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ReelManager reel1, reel2, reel3;
    private int completedReels = 0;
    internal static int playerCoin,betAmmount;
    public static GameManager Instance;
    public static bool machineSpinning;
    public UIManager uIManager;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        reel1.Init();
        reel2.Init();
        reel3.Init();
        betAmmount = 0;
        machineSpinning = false;
    }
    /// <summary>
    /// Slot Machine Spining Start Controller
    /// </summary>
    public void MachineSpinStart()
    {
        completedReels = 0;
        (float duration1, float speed1) = GenerateRandomValues(1.9f, 2.1f, 0.19f, 0.21f);
        (float duration2, float speed2) = GenerateRandomValues(2.9f, 3.1f, 0.24f, 0.26f);
        (float duration3, float speed3) = GenerateRandomValues(3.9f, 4.1f, 0.29f, 0.31f);
        reel1.SetDurationandSpeed(duration1, speed1);
        reel2.SetDurationandSpeed(duration2, speed2);
        reel3.SetDurationandSpeed(duration3, speed3);
        reel1.StartSpin();
        reel2.StartSpin();
        reel3.StartSpin();
    }
    /// <summary>
    /// Generate and return a random speed and random duration between its range 
    /// </summary>
    /// <returns></returns>
    private (float, float) GenerateRandomValues(float durationMin, float durationMax, float speedMin, float speedMax)
    {
        float randomDuration = Random.Range(durationMin, durationMax);
        float randomSpeed = Random.Range(speedMin, speedMax);
        return (randomDuration, randomSpeed);
    }
    /// <summary>
    /// On Reel Spinning task Complete 
    /// </summary>
    public void OnReelSpinComplete()
    {
        completedReels++;
        if (completedReels >= 3)
        {
            CheckWinCondition();
        }
    }
    /// <summary>
    /// Check the Win Condition
    /// </summary>
    private void CheckWinCondition()
    {
        Sprite reel1Sprite = reel1.reelImages[2].GetComponent<UnityEngine.UI.Image>().sprite;
        Sprite reel2Sprite = reel2.reelImages[2].GetComponent<UnityEngine.UI.Image>().sprite;
        Sprite reel3Sprite = reel3.reelImages[2].GetComponent<UnityEngine.UI.Image>().sprite;

        if (reel1Sprite == reel2Sprite && reel2Sprite == reel3Sprite)
        {
           // Debug.Log("u won");
            Instance.uIManager.SetResultTxt(Instance.uIManager.won);
            playerCoin += betAmmount * 2;
            Instance.uIManager.UpdatePlayerCoins();
            betAmmount = 0;
        }
        else
        {
           //Debug.Log("u loss");
            Instance.uIManager.SetResultTxt(Instance.uIManager.lose);
            betAmmount = 0;
        }
        GameManager.machineSpinning = false;
        if (playerCoin>=10)
            GameManager.Instance.uIManager.ToggelBetOption(true);
        
    }
}
