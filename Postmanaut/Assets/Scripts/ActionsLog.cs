using UnityEngine;
using UnityEngine.UI;

public class ActionsLog : MonoBehaviour
{

    private Text uiText;

    void Start()
    {
        uiText = GetComponent<Text>();
    }

    public void AddEntry(ActionCommand action)
    {
        uiText.text +=
            $"User: {action.user}\n" +
            $"Perform: {action.perform}\n" +
            $"Direction: {action.direction}\n\n";
    }

}
