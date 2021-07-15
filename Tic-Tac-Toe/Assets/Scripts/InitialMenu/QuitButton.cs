using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitButton : MonoBehaviour
{
    public void Button_Quit()
    {
        Application.Quit();
    }
}
