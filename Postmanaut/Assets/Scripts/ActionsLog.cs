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
        uiText.text += $"\n{action.user} Performed:\n{action.perform} - {action.direction}\n";
    }

}
