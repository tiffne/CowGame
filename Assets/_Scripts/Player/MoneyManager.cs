using UnityEngine;
using System.Collections.Generic;
using _Scripts.Customer;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyDisplay;

    public static MoneyManager Instance { get; private set; }
    
    public float totalTips { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Call this when a customer pays!
    public void AddTip(Customer customer)
    {
        float tip = customer.TipAmount;
        totalTips += tip;
        //
        // Debug.Log($"Added {tip} tip from {customer.Species} " +
        //          $"(Patience: {customer.patienceLevel}). " +
        //          $"Total tips: {totalTips}");
        UpdateMoneyDisplay();
    }

    private void UpdateMoneyDisplay()
    {
        moneyDisplay.text = $"{totalTips:C}";
    }
}