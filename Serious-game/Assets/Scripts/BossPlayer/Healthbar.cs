using UnityEngine;
using UnityEngine.UI;

namespace BossPlayer
{
    public class Healthbar : MonoBehaviour
    {
        public Transform bar;
        public Slider slider;

        public void SetHealth(int health)
        {
            slider.value = health;
        }
    
        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;
        }
    }
}
