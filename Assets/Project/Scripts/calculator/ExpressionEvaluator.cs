using System.Collections.Generic;

public static class ExpressionEvaluator
{
    // Main DMAS evaluation function
    public static float Evaluate(string expression)
    {
        if (string.IsNullOrEmpty(expression))
            return 0;

        List<float> numbers = new List<float>();
        List<char> operators = new List<char>();

        ParseExpression(expression, numbers, operators);
        ApplyDivisionAndMultiplication(numbers, operators);
        return ApplyAdditionAndSubtraction(numbers, operators);
    }

    // -----------------------------
    // Step 1: Parse expression 
    // -----------------------------
    private static void ParseExpression(string expr, List<float> nums, List<char> ops)
    {
        string buffer = "";

        foreach (char c in expr)
        {
            if (char.IsDigit(c) || c == '.')
            {
                buffer += c;
            }
            else if ("+-*/".Contains(c.ToString()))
            {
                nums.Add(float.Parse(buffer));
                buffer = "";
                ops.Add(c);
            }
        }

        nums.Add(float.Parse(buffer)); // last number
    }

    // -----------------------------
    // Step 2: DMAS (first / then *)
    // -----------------------------
    private static void ApplyDivisionAndMultiplication(List<float> nums, List<char> ops)
    {
        for (int i = 0; i < ops.Count; i++)
        {
            char op = ops[i];

            if (op == '/' || op == '*')
            {
                float a = nums[i];
                float b = nums[i + 1];
                float result = (op == '/') ? a / b : a * b;

                nums[i] = result;
                nums.RemoveAt(i + 1);
                ops.RemoveAt(i);
                i--; // stay at same index
            }
        }
    }

    // -----------------------------
    // Step 3: AS (left to right)
    // -----------------------------
    private static float ApplyAdditionAndSubtraction(List<float> nums, List<char> ops)
    {
        float result = nums[0];

        for (int i = 0; i < ops.Count; i++)
        {
            if (ops[i] == '+')
                result += nums[i + 1];
            else
                result -= nums[i + 1];
        }

        return result;
    }
}
