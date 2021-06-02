using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI label;
    [SerializeField]
    private TextMeshProUGUI endLabel;
    [SerializeField]
    private GameObject wqinPanel;


    [SerializeField]
    private int score;

	private void Start()
	{
        showScore();
	}

	void showScore()
	{
        label.text = "SCORE: " + score;
        endLabel.text = "Your score: " + score;
    }

    public void addToScore(int points)
	{
        score += points;
        showScore();
        if(score>430)
		{
            wqinPanel.SetActive(true);
		}
	}

}
