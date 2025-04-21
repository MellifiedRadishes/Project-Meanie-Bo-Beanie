using System.Drawing;
using TMPro;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    public TMP_Text numberText; // Assign in Inspector
    public float displayDuration = .8f; // Seconds
    public UnityEngine.Color color = UnityEngine.Color.white;

    private void Start()
    {
        numberText.gameObject.SetActive(false); // Start hidden
    }

    public void ShowNumber(int number, UnityEngine.Color color)
    {
        numberText.text = number.ToString();
        numberText.gameObject.SetActive(true);
        numberText.color = color;
        //StopAllCoroutines(); // Optional: Stop any previous display
        StartCoroutine(HideAfterDelay());
    }

    public void ShowText(string message, UnityEngine.Color color)
    {
        numberText.text = message;
        numberText.gameObject.SetActive(true);
        numberText.color = color;
        //StopAllCoroutines();
        StartCoroutine(HideAfterDelay());
    }

    private System.Collections.IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        numberText.gameObject.SetActive(false);
    }
}
