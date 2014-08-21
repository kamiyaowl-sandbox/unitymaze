using UnityEngine;
using System.Collections;

public class ActionPointDetect : MonoBehaviour {
	public int Level = 1;
	public GUIStyle guiSkin;

	private MakeStageScript makeStage;
	private System.DateTime startTime;
	private System.DateTime goalTime;
	private double PlayTime { get { return (goalTime - startTime).TotalSeconds; } }

	private bool isStart = false;
	private bool isGoal = false;
	private bool isHintShown = false;
	// Use this for initialization
	void Start() {
		var halo = GetComponent("Halo") as Behaviour;
		halo.enabled = false;
		makeStage = GameObject.FindWithTag("Stage").GetComponent("MakeStageScript") as MakeStageScript;
	}

	private IEnumerator gameStartTextShow() {
		var messageText = GameObject.Find("MessageText");
		var typeOfScript = messageText.GetComponent("TypeOutScript") as TypeOutScript;
		typeOfScript.FinalText = "Game Start!";
		startTime = System.DateTime.Now;
		typeOfScript.On = true;
		yield return new WaitForSeconds(3.0f);
		typeOfScript.reset = true;
	}
	private IEnumerator gameHintMakeShow() {
		var messageText = GameObject.Find("MessageText");
		var typeOfScript = messageText.GetComponent("TypeOutScript") as TypeOutScript;
		typeOfScript.FinalText = "Hint Shown!";
		typeOfScript.On = true;
		yield return new WaitForSeconds(3.0f);
		typeOfScript.reset = true;
	}


	// Update is called once per frame
	void Update() {
		//UpdateTime
		if (!isGoal) goalTime = System.DateTime.Now;
		if (isStart) {
			var timeText = GameObject.Find("TimeText");
			timeText.guiText.text = string.Format("TIME:{0}", PlayTime);
		}
	}
	void OnTriggerEnter(Collider col) {
		var halo = GetComponent("Halo") as Behaviour;
		halo.enabled = true;
	}
	void OnTriggerStay(Collider col) {
		switch (col.gameObject.tag) {
			case "Start":
				//Debug.Log("Start");
				break;
			case "Goal":
				Debug.Log("Goal");
				if (!isGoal) {
					isGoal = true;
					var messageText = GameObject.Find("MessageText");
					var typeOfScript = messageText.GetComponent("TypeOutScript") as TypeOutScript;
					typeOfScript.FinalText = "Game Clear!";
					typeOfScript.On = true;
				}
				break;
			case "Hint":
				if (!isHintShown) {
					isHintShown = true;
					makeStage.MakeHint();
					StartCoroutine(gameHintMakeShow());
				}
				break;
		}
	}
	void OnTriggerExit(Collider col) {
		var halo = GetComponent("Halo") as Behaviour;
		halo.enabled = false;
	}
	void OnGUI() {
		if (!isStart) {
			int level = 0;
			GUI.Button(new Rect(0, 0, 400, 100), "Select Level", guiSkin);
			if (GUI.Button(new Rect(25, 120, 250, 100), "Level 1", guiSkin)) {
				level = 1;
			}
			if (GUI.Button(new Rect(225, 220, 250, 100), "Level 2", guiSkin)) {
				level = 2;
			}
			if (GUI.Button(new Rect(425, 320, 250, 100), "Level 3", guiSkin)) {
				level = 3;
			}
			if (level > 0) {
				Level = level;
				/* Level Selected : Start Game */
				makeStage.Width = Random.Range(10 * Level, 20 * Level);
				makeStage.Height = Random.Range(10 * Level, 20 * Level);
				makeStage.Make();
				StartCoroutine(gameStartTextShow());
				isStart = true;
			}
		}
		if (isGoal) {
			if (GUI.Button(new Rect(50, 50, 250, 100), "Replay", guiSkin)) {
				Application.LoadLevel(0);
			}
			if (GUI.Button(new Rect(450, 50, 250, 100), "Tweet", guiSkin)) {
				var tweetText = string.Format("レベル{0}を{1}秒でクリアしました。 #Unityちゃん迷路を翔る {2}", Level, (int)PlayTime, "http://unityroom.com/games/play/246");
				Application.OpenURL(string.Format("https://twitter.com/intent/tweet?text={0}", WWW.EscapeURL(tweetText)));
			}
		}
	}
}
