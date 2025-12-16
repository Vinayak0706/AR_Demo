using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI instructionText;

    public void SetInstruction(string message)
    {
        instructionText.text = message;
        instructionText.gameObject.SetActive(true);
    }

    public void HideInstruction()
    {
        instructionText.gameObject.SetActive(false);
    }
}
