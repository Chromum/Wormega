using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public List<Difficulty> DifficultyList = new List<Difficulty>();
    public Dictionary<RectTransform, Difficulty> DifficultyDictionary = new Dictionary<RectTransform, Difficulty>();
    public GameObject baseObject;
    public Transform Slider;

    [Button]
    public void SetUpUI()
    {
        foreach (Difficulty item in DifficultyList)
        {
            RectTransform t = GameObject.Instantiate(baseObject).GetComponent<RectTransform>();
            DifficultyDictionary.Add(t, item);
            t.sizeDelta = new Vector2(item.Length, t.sizeDelta.y);
            t.parent = this.transform;
            t.transform.position = this.transform.localPosition;
            t.GetComponent<Image>().color = item.difficultyColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Slider.position = new Vector3(Slider.position.x - Time.deltaTime, Slider.position.y, Slider.position.z);
    }
}
