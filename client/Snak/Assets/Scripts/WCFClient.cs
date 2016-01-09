using UnityEngine;
using System.Collections;
using System.ServiceModel;

/// <summary>
/// A proxy class for the generated WCF client class. It makes a single client instance available throughout the scene.
/// </summary>
public class WCFClient : MonoBehaviour {

    public ServerServiceClient client;

	void Start () {
        client = new ServerServiceClient(new BasicHttpBinding(BasicHttpSecurityMode.None), new EndpointAddress("http://localhost:8000/Snak/ServerService"));
    }
	
    public void OnDestroy()
    {
        client.Close();
    }
}
