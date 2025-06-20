using UnityEngine;

public class MusicController : MonoBehaviour
{
    static bool exists;

    void Start()
    {
        if (exists)
        {
            Destroy(this.gameObject);
            return;
        }

        exists = true;
        DontDestroyOnLoad(this.gameObject);
    }
}
