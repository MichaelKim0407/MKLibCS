using UnityEngine;

namespace MKLibCS.Unity
{
    /// <summary>
    /// </summary>
    public static class UnityUtil
    {
        /// <summary>
        /// </summary>
        /// <param name="gameObject"></param>
        public static void DontDestroyOnLoad(this GameObject gameObject)
        {
            Object.DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Destroy(this GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }

        /// <summary>
        /// </summary>
        /// <param name="gameObject"></param>
        public static void SafeDestroy(this GameObject gameObject)
        {
            gameObject.transform.SetParent(null);
            gameObject.SetActive(false);
            gameObject.Destroy();
        }
    }
}