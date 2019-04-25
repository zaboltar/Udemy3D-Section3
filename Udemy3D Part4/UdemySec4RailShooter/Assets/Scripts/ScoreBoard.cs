using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreBoard : MonoBehaviour
{
    
    int score;
    Text scoreTxt;
    
    void Start()
    {
        scoreTxt = GetComponent<Text>();
        scoreTxt.text = "Score: " + score.ToString();
    }

  public void ScoreHit(int scorePerHit)
  {
      score = score + scorePerHit;
      scoreTxt.text = "Score: " + score.ToString();
  }
}
