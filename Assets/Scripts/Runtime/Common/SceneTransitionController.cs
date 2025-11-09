using System;
using NewKris.Runtime.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewKris.Runtime.Common {
    public class SceneTransitionController : MonoBehaviour {
        private static SceneTransitionController Instance;

        public static void LoadScene(GameScene scene) {
            SceneManager.LoadScene((int)scene);
        }
        
        private void Awake() {
            Singleton.SetSingleton(ref Instance, this);
        }

        private void OnDestroy() {
            Singleton.UnsetSingleton(ref Instance, this);
        }
    }
}
