using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class FPS : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshPro Text;
 
    private Dictionary<int, string> CachedNumberStrings = new();
    private int[] _frameRateSamples;
    private int _cacheNumbersAmount = 300;
    private int _averageFromAmount = 30;
    private int _averageCounter = 0;
    private int _currentAveraged;
 
    void Awake()
    {
        // Cache strings and create array
        {
            for (int i = 0; i < _cacheNumbersAmount; i++) {
                CachedNumberStrings[i] = i.ToString();
            }
            _frameRateSamples = new int[_averageFromAmount];
        }
        DontDestroyOnLoad(this.gameObject);

    }
    void Update()
    {
        // Sample
        {
            var currentFrame = (int)Mathf.Round(1f / Time.smoothDeltaTime); // If your game modifies Time.timeScale, use unscaledDeltaTime and smooth manually (or not).
            _frameRateSamples[_averageCounter] = currentFrame;
        }
 
        // Average
        {
            var average = 0f;
 
            foreach (var frameRate in _frameRateSamples) {
                average += frameRate;
            }
 
            _currentAveraged = (int)Mathf.Round(average / _averageFromAmount);
            _averageCounter = (_averageCounter + 1) % _averageFromAmount;
        }
 
        // Assign to UI
        {
            Text.text = _currentAveraged < _cacheNumbersAmount && _currentAveraged > 0
                ? CachedNumberStrings[_currentAveraged]
                : _currentAveraged < 0
                    ? "< 0"
                    : _currentAveraged > _cacheNumbersAmount
                        ? $"> {_cacheNumbersAmount}"
                        : "-1";
        }
    }
}
