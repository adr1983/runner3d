using UnityEngine;

/*quando compilado some da tela*/
public class PauseNow : MonoBehaviour
{
    void OnGUI()
    {
        if (GUILayout.Button("Pause Now!"))
        {
            Debug.Break();
        }
    }
}