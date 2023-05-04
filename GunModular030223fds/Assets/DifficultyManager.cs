using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public Difficulty CurrentDifficulty;
    public List<Difficulty> DifficultyList = new List<Difficulty>();
    public Dictionary<RectTransform, Difficulty> DifficultyDictionary = new Dictionary<RectTransform, Difficulty>();
    public GameObject baseObject;
    public Transform Slider;
    public float inGameTime;
    public float timeSinceLastDifficultyChange;
    public GameObject G;

    [Button]
    public void SetUpUI()
    {
        foreach (Difficulty item in DifficultyList)
        {
            RectTransform t = GameObject.Instantiate(baseObject).GetComponent<RectTransform>();
            DifficultyDictionary.Add(t, item);
            t.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.difficultyName;
            t.sizeDelta = new Vector2(item.Length, t.sizeDelta.y);
            t.parent = this.transform;
            t.transform.position = this.transform.localPosition;
            t.GetComponent<Image>().color = item.difficultyColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        G.GetComponent<Image>().material = CurrentDifficulty.Mat;

    }

    // Update is called once per frame
    void Update()
    {



        if (timeSinceLastDifficultyChange >= CurrentDifficulty.Length)
        {
            CurrentDifficulty = DifficultyList[DifficultyList.IndexOf(CurrentDifficulty) + 1];
            GameManager.instance.currentDifficulty = CurrentDifficulty;
            G.GetComponent<Image>().material = CurrentDifficulty.Mat;
            timeSinceLastDifficultyChange = 0f;
        }

    }
}
