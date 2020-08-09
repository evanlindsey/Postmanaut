using System;
using System.Linq;
using System.Text.RegularExpressions;
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

        srLib.ConnectionStarted += (object sender, ConnectionEventArgs e) =>
        {
            string hubName = signalRHubUrl.Split('/').Last();
            string endpointUrl = signalRHubUrl.Replace(hubName, e.ConnectionId);

            input.DisplayMessage(endpointUrl);
            Debug.Log(endpointUrl);
        };

        srLib.MessageReceived += (object sender, MessageEventArgs e) =>
        {
            ActionCommand action = new ActionCommand();
            action = JsonUtility.FromJson<ActionCommand>(e.Message);

            player.PerformAction(action.perform);
            player.TurnDirection(action.direction);
        };
    }

    [Serializable]
    public class ActionCommand
    {
        public string perform;
        public string direction;
    }

}
