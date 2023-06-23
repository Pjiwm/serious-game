using System;

namespace FishingMinigame
{
    [Serializable]
    public class Fish
    {
        public string name;
        public string spriteID;
        public int baseCost;
        public int spokeWeight;

        public Fish(string a, string b, int c, int d) {
            name = a;
            spriteID = b;
            baseCost = c;
            spokeWeight = d;
        }
    }
}
