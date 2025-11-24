using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ColorMixManager : MonoBehaviour
{
    [System.Serializable]
    public class ColorSprite
    {
        public string name;
        public Sprite sprite;
    }

    [System.Serializable]
    public class MixResult
    {
        public string mixName;
        public Sprite resultSprite;
    }

    [Header("UI Elements")]
    public Image slot1, slot2, resultImage;
    public List<Button> optionButtons; // 3 Buttons
    //public Text roundText; // Optional (Assign in Inspector)

    [Header("Color Sprites")]
    public List<ColorSprite> baseColors;

    [Header("Mix Results")]
    public List<MixResult> mixedResults;

    private string selected1 = "", selected2 = "";
    private List<ColorSprite> currentOptions = new List<ColorSprite>();

    private int currentRound = 0;
    private int totalRounds = 5;

    void Start()
    {
        StartNextRound();
    }

    void StartNextRound()
    {
        if (currentRound >= totalRounds)
        {
            Debug.Log("🎉 All rounds completed!");
            //roundText.text = "Game Over!";
            // You can trigger restart here or show game over panel
            return;
        }

        currentRound++;
        //roundText.text = "Round: " + currentRound + " / " + totalRounds;
        GenerateRandomColorOptions();
    }

    public void GenerateRandomColorOptions()
    {
        selected1 = "";
        selected2 = "";
        slot1.sprite = null;
        slot2.sprite = null;
        resultImage.sprite = null;

        currentOptions.Clear();

        List<ColorSprite> shuffled = new List<ColorSprite>(baseColors);
        Shuffle(shuffled);

        for (int i = 0; i < 3; i++)
        {
            currentOptions.Add(shuffled[i]);

            Image img = optionButtons[i].GetComponent<Image>();
            img.sprite = shuffled[i].sprite;

            string colorName = shuffled[i].name;

            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => SelectColor(colorName));
        }
    }

    public void SelectColor(string colorName)
    {
        if (selected1 == "")
        {
            selected1 = colorName;
            slot1.sprite = GetBaseColorSprite(colorName);
        }
        else if (selected2 == "" && colorName != selected1)
        {
            selected2 = colorName;
            slot2.sprite = GetBaseColorSprite(colorName);
            ShowResult();

            // Move to next round after a delay
            Invoke(nameof(StartNextRound), 2f); // 2-second delay before next round
        }
    }

    void ShowResult()
    {
        string key = GetSortedKey(selected1, selected2);

        foreach (var mix in mixedResults)
        {
            if (mix.mixName == key)
            {
                resultImage.sprite = mix.resultSprite;
                return;
            }
        }

        Debug.LogWarning("No matching result for: " + key);
    }

    Sprite GetBaseColorSprite(string name)
    {
        foreach (var color in baseColors)
        {
            if (color.name == name)
                return color.sprite;
        }
        return null;
    }

    string GetSortedKey(string a, string b)
    {
        return string.Compare(a, b) < 0 ? $"{a}_{b}" : $"{b}_{a}";
    }

    void Shuffle(List<ColorSprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            ColorSprite temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    public void ResetGame()
    {
        currentRound = 0;
        StartNextRound();
    }
}
