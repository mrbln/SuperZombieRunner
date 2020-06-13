using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{

	public GameObject playerPrefab;
	public Text continueText;
	public Text scoreText;
	public Button pauseButton;

	private float timeElapsed = 0f;
	private float bestTime = 0f;
	private float blinkTime = 0f;
	private bool blink;
	private bool gameStarted;
	private TimeManager timeManager;
	private GameObject player;
	private GameObject floor;
	private Spawner spawner;
	private bool beatBestTime;
	private bool isPlayerKilled;

	void Awake()
	{
		floor = GameObject.Find("Foreground");
		spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
		timeManager = GetComponent<TimeManager>();
	}

	// Use this for initialization
	void Start()
	{

		var floorHeight = floor.transform.localScale.y;

		var pos = floor.transform.position;
		pos.x = 0;
		pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2) + (floorHeight / 2);
		floor.transform.position = pos;

		Time.timeScale = 0;

		continueText.text = "PRESS ANY BUTTON TO START";

		bestTime = PlayerPrefs.GetFloat("BestTime");
	}

	// Update is called once per frame
	void Update()
	{
		if (!gameStarted && Time.timeScale == 0 && !isPlayerKilled)
		{
			timeManager.ManipulateTime(1, 1f);
			ResetGame();
		} else
		{
			if (isPlayerKilled && Input.anyKeyDown)
			{
				isPlayerKilled = false;
				timeManager.ManipulateTime(1, 1f);
				pauseButton.interactable = true;
				ResetGame();
			}
		}

		if (!gameStarted)
		{
			blinkTime++;
			
			if (blinkTime % 40 == 0)
			{
				blink = !blink;
			}

			continueText.canvasRenderer.SetAlpha(blink ? 0 : 1);

			var textColor = beatBestTime ? "#FF0" : "#FFF";

			scoreText.text = "TIME: " + FormatTime(timeElapsed) + 
				"\n<color=" + textColor + ">BEST: " + FormatTime(bestTime) + "</color>";
		} else
		{
			timeElapsed += Time.deltaTime;
			scoreText.text = "TIME: " + FormatTime(timeElapsed);
		}
	}

	void OnPlayerKilled()
	{
		spawner.active = false;
		isPlayerKilled = true;

		var playerDestroyScript = player.GetComponent<DestroyOffscreen>();
		playerDestroyScript.DestroyCallback -= OnPlayerKilled;

		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		timeManager.ManipulateTime(0, 5.5f);
		gameStarted = false;

		continueText.text = "PRESS ANY BUTTON TO RESTART";
		pauseButton.interactable = false;

		if (timeElapsed > bestTime)
		{
			bestTime = timeElapsed;
			PlayerPrefs.SetFloat("BestTime", bestTime);

			beatBestTime = true;
		}
	}

	void ResetGame()
	{
		spawner.active = true;

		player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0, (Screen.height / PixelPerfectCamera.pixelsToUnits) / 2 + 100, 0));

		var playerDestroyScript = player.GetComponent<DestroyOffscreen>();
		playerDestroyScript.DestroyCallback += OnPlayerKilled;

		gameStarted = true;

		continueText.canvasRenderer.SetAlpha(0);

		timeElapsed = 0;

		beatBestTime = false;
	}

	string FormatTime(float value)
	{
		TimeSpan t = TimeSpan.FromSeconds(value);

		return string.Format("{0:D2}:{1:D2}.{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);
	}
}
