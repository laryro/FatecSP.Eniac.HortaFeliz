using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    private int actualScore = 0;
    private int oldScore = 0;
	
    public void Update()
    {
        if( oldScore != actualScore)
        {
            if (actualScore < 0) actualScore = 0;
            this.gameObject.transform.GetChild(1).GetComponent<Text>().text = actualScore.ToString("D5");
            oldScore = actualScore;
        }
    }
    public void AddPoints(int val)
    {
        actualScore += val;
    }
    public void SubtractPoints(int val)
    {
        actualScore -= 5 * val;
    }
}
