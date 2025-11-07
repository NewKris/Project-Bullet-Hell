using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewKris.Runtime.Ui {
    public class MenuController : MonoBehaviour {
        public void ReloadGameplay() {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
