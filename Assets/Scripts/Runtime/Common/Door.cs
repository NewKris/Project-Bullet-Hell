using UnityEngine;

namespace Werehorse.Runtime.Common {
    public class Door : MonoBehaviour {
        public void GoToScene(int sceneIndex) {
            SceneTransitionController.LoadScene(sceneIndex);
        }
    }
}
