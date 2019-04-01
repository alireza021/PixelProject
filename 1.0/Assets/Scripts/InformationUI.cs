using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationUI : MonoBehaviour
{
	public Text stateLabel;

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
	{
		if (!gameObject.GetComponentInParent<EnemyAI> ().dead) {
			Vector3 statePos = Camera.main.WorldToScreenPoint (this.transform.position);
			stateLabel.transform.position = statePos;
		} else {
			stateLabel.text = "";
		}
	}

}
