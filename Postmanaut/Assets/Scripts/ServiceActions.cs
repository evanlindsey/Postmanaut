using System.Linq;
using UnityEngine;

public class ServiceActions : MonoBehaviour
{

    public string signalRHubUrl = "https://postmanaut.herokuapp.com/ActionHub";
    public string hubListenerName = "ReceiveAction";

    private PlayerControl player;
    private EndpointBar endpoint;
    private ActionsLog actions;
    private SignalRLib srLib;

    void Start()
    {
        player = GameObject.Find("Postmanaut").GetComponent<PlayerControl>();
        endpoint = GameObject.Find("EndpointBar").GetComponent<EndpointBar>();
        actions = GameObject.Find("ActionsLog").GetComponent<ActionsLog>();

        srLib = new SignalRLib();
        srLib.Init(signalRHubUrl, hubListenerName);

        srLib.ConnectionStarted += (object sender, ConnectionEventArgs e) =>
        {
            string hubName = signalRHubUrl.Split('/').Last();
            string endpointUrl = signalRHubUrl.Replace(hubName, e.ConnectionId);

            Debug.Log(endpointUrl);
            endpoint.UpdateURL(endpointUrl);
        };

        srLib.MessageReceived += (object sender, MessageEventArgs e) =>
        {
            ActionCommand action = new ActionCommand();
            action = JsonUtility.FromJson<ActionCommand>(e.Message);

            if (!string.IsNullOrEmpty(action.direction))
                player.SetDirection(action.direction);

            if (!string.IsNullOrEmpty(action.perform))
                player.PerformAction(action.perform);

            actions.AddEntry(action);
        };
    }

}
