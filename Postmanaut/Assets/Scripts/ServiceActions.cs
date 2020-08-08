using System.Linq;
using UnityEngine;

public class ServiceActions : MonoBehaviour
{

    public string signalRHubUrl = "http://localhost:5000/ActionHub";
    public string hubListenerName = "ReceiveAction";

    private PlayerControl player;
    private InputText input;
    private SignalRLib srLib;

    void Start()
    {
        player = GameObject.Find("Postmanaut").GetComponent<PlayerControl>();
        input = GameObject.Find("InputField").GetComponent<InputText>();

        srLib = new SignalRLib();
        srLib.Init(signalRHubUrl, hubListenerName);

        srLib.ConnectionStarted += (object sender, MessageEventArgs e) =>
        {
            string connectionId = e.Message;
            string hubName = signalRHubUrl.Split('/').Last();
            string endpointUrl = signalRHubUrl.Replace(hubName, connectionId);

            input.DisplayMessage(endpointUrl);
            Debug.Log(e.Message);
        };

        srLib.MessageReceived += (object sender, MessageEventArgs e) =>
        {
            string[] command = e.Message.Split(',');
            string perform = command[0];
            string direction = command[1];

            player.PerformAction(perform);
            player.TurnDirection(direction);
        };
    }

}
