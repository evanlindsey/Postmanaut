using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour
{

    private InputField input;

    void Start()
    {
        input = GetComponent<InputField>();
    }

    public void DisplayMessage(string message)
    {
        input.text = message;
    }

}
