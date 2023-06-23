using UnityEngine;

namespace EmojiShooter
{
    public class BeamController : MonoBehaviour
    {
        public float speed = 50;

        private void Update()
        {
            transform.Translate(Vector2.right * (speed * Time.deltaTime));
        }
    }
}
