
using UnityEngine;
using UnityEngine.UI;

public class HorizontalBar : MonoBehaviour
{
    [Header("Bar References")]
    public Image bar;

    // Changes Amount In Bar
    public void changeAmount(float amount) {
        bar.fillAmount = amount;
    }
}
