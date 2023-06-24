using System;

namespace SceneLoading
{
    public static class SceneLoader
    {
        public enum Scenes
        {
            EmojiShooter,
            Level1,
            Level3,
            EndBoss,
            Loading,
            Level2,
            Endscreen,
            StartMenu
        }

        private static Action _onLoaderCallback;

        public static void LoadScene(Scenes scenes)
        {
            _onLoaderCallback = () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(scenes.ToString());
            };
            UnityEngine.SceneManagement.SceneManager.LoadScene(Scenes.Loading.ToString());
        }

        public static void LoaderCallback()
        {
            _onLoaderCallback?.Invoke();
            _onLoaderCallback = null;
        }
    }
}
