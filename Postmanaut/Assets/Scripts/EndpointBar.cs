using UnityEngine;
using UnityEngine.UI;

public class EndpointBar : MonoBehaviour
{

    private InputField uiInput;

    void Start()
    {
        uiInput = GetComponent<InputField>();
    }

    public void UpdateURL(string endpointUrl)
    {
        uiInput.text = endpointUrl;
    }

}
