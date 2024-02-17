using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private int nextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
            SceneLoader.load?.Invoke(nextScene);
    }
}
