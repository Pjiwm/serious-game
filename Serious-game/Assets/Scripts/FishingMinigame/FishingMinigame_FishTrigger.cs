using System;
using System.Collections;
using System.Collections.Generic;
using FishingMinigame;
using UnityEngine;

public class FishingMinigameFishTrigger : MonoBehaviour
{
   /// <summary>
   /// We use this to detect whether or not the fish is in the catching bar
   /// and sends that info to the FishingMinigame.cs script.
   /// </summary>
   
   public bool beingCaught = false;
   private FishingMinigameController _minigameController;
   private FishingMinigameController[] _minigameControllers;
   private void Start() {
      _minigameControllers = FindObjectsOfType<FishingMinigameController>();
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

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (!_minigameController.ReelingFishState) return;
      if (!other.CompareTag("CatchingBar") || beingCaught) return;
      
      beingCaught = true;
      _minigameController.FishInBar();
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (!other.CompareTag("CatchingBar") || !beingCaught) return;
      
      beingCaught = false;
      _minigameController.FishOutOfBar();
   }
}
