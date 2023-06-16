using System.Collections;
using UnityEngine;

namespace DefaultNamespace.SceneLoading
{
    /// <summary>
    /// This callback is used by the "Loader" Scene (which is a loader scene), and is called when the scene is loaded.
    /// </summary>
    public class LoaderCallback: MonoBehaviour
    {
        private bool _isFirstUpdate = true;

        private void Update()
        {
            if (_isFirstUpdate)
            {
                StartCoroutine(LoadCallbackCoroutine());
            }
        }

        private IEnumerator LoadCallbackCoroutine()
        {
            _isFirstUpdate = false;
            yield return new WaitForSeconds(0.2f);
            SceneLoader.LoaderCallback();
        }
    }
}