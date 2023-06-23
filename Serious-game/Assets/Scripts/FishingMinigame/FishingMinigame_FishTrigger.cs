using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMinigameFishTrigger : MonoBehaviour
{
   /// <summary>
   /// We use this to detect whether or not the fish is in the catching bar
   /// and sends that info to the FishingMinigame.cs script.
   /// </summary>
   
   public bool beingCaught = false;
   private FishingMinigame _minigameController;
   private FishingMinigame[] _minigameControllers;
   private void Start() {
      _minigameControllers = FindObjectsOfType<FishingMinigame>();
      foreach (var minigameController in _minigameControllers) {
         if (minigameController.ReelingFishState) {
            _minigameController = minigameController;
         }
      }
   }
   private void Update() {
      foreach (var minigameController in _minigameControllers) {
         if (minigameController.ReelingFishState) {
            _minigameController = minigameController;
         }
      }
   }

   private void OnTriggerEnter2D(Collider2D other) {
      if (_minigameController.ReelingFishState) {
         if (other.CompareTag("CatchingBar") && !beingCaught) {
            beingCaught = true;
            _minigameController.FishInBar();
         }
      }
   }

   private void OnTriggerExit2D(Collider2D other) {
      if (other.CompareTag("CatchingBar") && beingCaught) {
         beingCaught = false;
         _minigameController.FishOutOfBar();
      }
   }
}
