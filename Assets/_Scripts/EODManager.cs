using UnityEngine;
using TMPro;

public class EODManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI eodDisplay;

    void Start()
    {
        eodDisplay.text = $"Today's Earnings: ${MoneyManager.Instance.totalTips:C}\n" +
                  $"Number of Customers Served: {MoneyManager.Instance.TotalServedCustomer}\n" +
                  $"Number of Customers Lost: {MoneyManager.Instance.TotalLostCustomer}\n\n" +
                  "THANKS FOR PLAYING";
    }
}
