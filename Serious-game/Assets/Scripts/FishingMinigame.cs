using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FishingMinigame : MonoBehaviour
{
	/// <summary>
	/// This is the main fishing minigame code. It controls the states of fishing,
	/// then when in the minigame state it controls the movement of the catching
	/// bar and selects which fish we are catching.
	/// </summary>

	//These are references for the gameobjects used in the UI
	[Header("Game objects")]

    [SerializeField] private GameObject catchAreaBox;


    [SerializeField] private GameObject fish;
    [SerializeField] private Slider catchProgressBar;

    [SerializeField] private GameObject thoughtBubbles;

    [SerializeField] private GameObject parentGameObject;
    [SerializeField] private GameStateController GameStateController;
    [Header("Scriptable Objects")]
    [SerializeField] private FishManager fishManager;
    [SerializeField] private PlayerInput playerInput;
    
    [Header("Variables")]
    [SerializeField] private float catchSpeedMultiplier = 10f; //Higher means catch fish faster x
    [SerializeField] private float catchingBarUpForce = 30000; //How much force to push the catchingbar up by
    [SerializeField] private float maxNibbleWaitTime = 10;
    
    public bool ReelingFishState { get; private set; }
    private FishingMinigameFishTrigger _fishTrigger; //Reference to this script on the fish
    private bool _inFishCatchAreaBox;
    private bool _fishingButtonIsActive;
    private float _catchPercentage;
    private Vector3 _catchingBarResetPosition;
    private Rigidbody2D _catchingBarRb;
    private Fish _currentFishOnLine; //Reference to the current fish we are catching (Fish class is in Fish.cs)
    
    private static readonly int Reset = Animator.StringToHash("Reset");
    private static readonly int Alert = Animator.StringToHash("Alert");

    //State variables
    private bool _lineCast;
    private bool _nibbleEvent;
    private bool _freezeInputs;
    
    
    private void Start()
    {
	    _catchingBarRb = catchAreaBox.GetComponent<Rigidbody2D>();
	    _catchingBarResetPosition = catchAreaBox.GetComponent<RectTransform>().localPosition; 
	    
	    
	    playerInput.OnFishing += PlayerInputOnOnFishing;
	    playerInput.OnFishingStarted += (sender, args) =>
	    {
		    _fishingButtonIsActive = true;
	    };
	    playerInput.OnFishingCanceled += (sender, args) => _fishingButtonIsActive = false;
    }

    public void StartMinigame()
    {
	    GameStateController.ChangeToState(GameState.OnMinigame);
	    CastLine();
    }

    private void PlayerInputOnOnFishing(object sender, EventArgs e)
    {
	    if (ReelingFishState) return;
	    
	    if (_lineCast && !_nibbleEvent) { //This is if the line has cast and we reel in before we get a nibble
		    StopAllCoroutines(); //Stops the WaitForNibble timer
		    
		    _lineCast = false;
		    
		    ResetThoughtBubbles();
		    EndGame();
		    return;
	    }
	    if (_lineCast && _nibbleEvent) {
		    StopAllCoroutines(); //Stops the LineBreak timer
		    StartReeling();
	    }
    }

    private void Update()
    {
	    //This is when we are in the stage where we are fighitng for the fish
	    if (_fishingButtonIsActive && ReelingFishState) { //If we press space
		    _catchingBarRb.AddForce(Vector2.up * (catchingBarUpForce * Time.deltaTime), ForceMode2D.Force); //Add force to lift the bar
	    }
	    
	    if (_inFishCatchAreaBox && ReelingFishState) {
		    _catchPercentage += catchSpeedMultiplier * Time.deltaTime;
	    } else {
		    _catchPercentage -= catchSpeedMultiplier * Time.deltaTime;
	    }
	    
	    FadeFish();
	    
	    //Clamps our percentage between 0 and 100
	    _catchPercentage = Mathf.Clamp(_catchPercentage, 0, 100);
	    catchProgressBar.value = _catchPercentage;
	    if (_catchPercentage >= 100) { //Fish is caught if percentage is full
		    FishCaught();
	    }
    }

    private void FadeFish()
    {
	    var fishColor = Color.Lerp(Color.black, Color.white, Map(0, 100, 0, 1, _catchPercentage));
	    fish.GetComponent<Image>().color = fishColor;
    }
    
    //Called to cast our line
    private void CastLine() {
	    _lineCast = true;
	    thoughtBubbles.SetActive(true);
	    StartCoroutine(WaitForNibble());
    }
    
    //Wait a random time to get a nibble
    private IEnumerator WaitForNibble() {
	    yield return new WaitForSeconds(Random.Range(maxNibbleWaitTime * 0.25f, maxNibbleWaitTime)); //Wait between 25% of maxWaitTime and the maxWaitTime
	    thoughtBubbles.GetComponent<Animator>().SetTrigger(Alert); //Show the alert thoughtbubble
	    _nibbleEvent = true; 
	    StartCoroutine(LineBreak(2)); //If we dont respond in 2 seconds break the line
    }
    
    private void StartReeling() {
	    parentGameObject.SetActive(true);
	    ReelingFishState = true;
	    
	    _nibbleEvent = false;
	    _lineCast = false;
	    
	    //Set up the fish we are catching
	    _currentFishOnLine = fishManager.GetRandomFishWeighted();
	    var tempSprite = Resources.Load<Sprite>($"FishSprites/{_currentFishOnLine.spriteID}"); //Get fish sprite from our resources file
	    fish.GetComponent<Image>().sprite = tempSprite;

	    //Changes the width and height of the fishBar to accomodate for wider sprites
	    var w = Map(0, 32, 0, 55, tempSprite.texture.width);
	    var h = Map(0, 32, 0, 55, tempSprite.texture.height);
	    fish.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
	    
	    parentGameObject.SetActive(true);
    }
    
    //This breaks the line if we are waiting for a response too long
    private IEnumerator LineBreak(float lineBreakTime) {
	    yield return new WaitForSeconds(lineBreakTime);
	    Debug.Log("Line Broke!");
	    
	    //Disable thought bubbles
	    ResetThoughtBubbles();
	    
	    _lineCast = false;
	    _nibbleEvent = false;
    }
    
    //Called from the FishingMinigame_FishTrigger script
    public void FishInBar() {
	    _inFishCatchAreaBox = true;
    }
    
	//Called from the FishingMinigame_FishTrigger script
    public void FishOutOfBar() {
	    _inFishCatchAreaBox = false;
    }
    
    public void FishCaught()
    {
	    ResetThoughtBubbles();
	    
	    _catchPercentage = 0;
	    parentGameObject.SetActive(false);
	    catchAreaBox.transform.localPosition = _catchingBarResetPosition;
	    
	    ReelingFishState = false;
	    
	    var fishCaughtDialog = new Dialog()
	    {
		    lines = new List<string> { "Je hebt een " + _currentFishOnLine.name + " gevangen!" }
	    };
	    EndGame();
	    
	    StartCoroutine(DialogManager.Instance.ShowDialog(fishCaughtDialog));
    }
    
    private void EndGame() {
	    GameStateController.ChangeToState(GameState.Roaming);
    }

    //Classic mapping script x
    private static float Map(float a, float b, float c, float d, float x) {
	    return (x - a) / (b - a) * (d - c) + c;
    }

    private void ResetThoughtBubbles()
    {
	    thoughtBubbles.GetComponent<Animator>().SetTrigger(Reset);
	    thoughtBubbles.SetActive(false);
    }
    
}