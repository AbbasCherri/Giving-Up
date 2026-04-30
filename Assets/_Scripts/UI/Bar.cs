using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Image image;

    public void BarProgress(float current, float max)
    {
        image.fillAmount = (float) current / max;
    }
}