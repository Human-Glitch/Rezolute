using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class PlayerStatManager: MonoBehaviour {
	public GameObject DataFragmentCounter {get; set; }

	public int Score {get; set; } = 0;

    private void Start()
    {
        DataFragmentCounter = gameObject;
    }

    public void IncreaseDataFragments()
	{
        DataFragmentCounter.GetComponent<Text>().text = string.Format("Data Fragments {0}", Score++);

		if (Score % 10 == 0)
        {
            Object.FindObjectOfType<LifeManager>().GainLife();
        }   
	}
		
}
