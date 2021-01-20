namespace ProjectSystem {
    public static class SceneManager {
        public static void Move(Scenes scenes) {
            UnityEngine.SceneManagement.SceneManager.LoadScene((int) scenes);
        }
    }
}
