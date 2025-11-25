using UnityEngine;
using TMPro;

public class CalculatorUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text expressionText;
    public TMP_Text resultText;

    private string expression = "";

    // -----------------------------
    // Number Buttons (0–9)
    // -----------------------------
    public void PressNumber(string num)
    {
        expression += num;
        UpdateUI();
    }

    // -----------------------------
    // Operator Buttons (+ - * /)
    // -----------------------------
    public void PressOperator(string op)
    {
        if (expression.Length == 0)
            return;

        char last = expression[expression.Length - 1];

        // Prevent double operators
        if ("+-*/".Contains(last.ToString()))
            expression = expression.Remove(expression.Length - 1);

        expression += op;
        UpdateUI();
    }

    // -----------------------------
    // Clear Last Character
    // -----------------------------
    public void PressClear()
    {
        if (expression.Length > 0)
        {
            expression = expression.Remove(expression.Length - 1);
            UpdateUI();
        }
    }

    // -----------------------------
    // Reset Everything
    // -----------------------------
    public void PressReset()
    {
        expression = "";
        resultText.text = "";
        UpdateUI();
    }

    // -----------------------------
    // Evaluate Expression (=)
    // -----------------------------
    public void PressEquals()
    {
        if (string.IsNullOrEmpty(expression))
            return;

        float result = ExpressionEvaluator.Evaluate(expression);
        resultText.text = result.ToString();
    }

    // -----------------------------
    // UI Update
    // -----------------------------
    private void UpdateUI()
    {
        expressionText.text = expression;
    }
}
