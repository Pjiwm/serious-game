using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FishingMinigame
{
    [CreateAssetMenu(fileName = "FishManager", menuName = "ScriptableObjects/FishManager", order = 1)]
    public class FishManager : ScriptableObject
    {
    
        /// <summary>
        /// This script takes the information from the CSV file with all of the fish
        /// information and creates a list of the Fish class with an instance for
        /// each fish in the csv file!
        /// </summary>
    
        [SerializeField] private List<Fish> allFish = new List<Fish>();

        public Fish GetRandomFish() {
            //Picks out a random fish from the list
            return allFish[Random.Range(0, allFish.Count)];
        }

        public Fish GetRandomFishWeighted() {
            //Picks out a random fish using spoke weights
            var totalSpoke = allFish.Sum(fish => fish.spokeWeight);
        
            var valueChosen = Random.Range(0, totalSpoke);
        
            foreach (var fish in allFish) {
                if (valueChosen < fish.spokeWeight) {
                    return fish;
                } else {
                    valueChosen -= fish.spokeWeight;
                }
            }
            return null;
        }
    }
}
