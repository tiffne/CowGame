using UnityEngine;
using _Scripts.Customer;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyDisplay;

    [SerializeField] private AudioSource moneySound;

    public static MoneyManager Instance { get; private set; }
    
    public float totalTips { get; private set; }
    public int TotalServedCustomer { get; set; } = 0;
    public int TotalLostCustomer { get; set; } = 0;
    
    
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
        moneySound.Play();
    }

    private void UpdateMoneyDisplay()
    {
        moneyDisplay.text = $"{totalTips:C}";
    }
}