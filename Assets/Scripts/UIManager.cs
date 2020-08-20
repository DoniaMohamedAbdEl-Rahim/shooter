using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Handle to text 
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite []_liveSprites;
    [SerializeField]
    private Image _livesImg;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score : " + 0;
    }
   public void UpdateScore(int playerScore)
    {
        _scoreText.text="Score : " +playerScore.ToString();
    }
    public void UpdateLives(int CurrentLive)
    {
        _livesImg.sprite = _liveSprites[CurrentLive];
    }
}
