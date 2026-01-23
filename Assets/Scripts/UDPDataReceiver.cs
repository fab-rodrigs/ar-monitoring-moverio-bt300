using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPDataReceiver : MonoBehaviour
{
    Thread receiveThread;
    UdpClient client;
    public int port = 5005; // Mesma porta configurada no LabVIEW
    public string lastReceivedPacket = "";
    public bool dataUpdate = false;

    void Start()
    {
        // Cria uma thread separada para não travar o Unity enquanto espera dados
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP); // Aguarda o pacote do LabVIEW
                lastReceivedPacket = Encoding.UTF8.GetString(data);
                dataUpdate = true; // Avisa que chegou um dado novo
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }
    }

    // Fecha a conexão quando você para o jogo no Unity
    void OnApplicationQuit()
    {
        if (receiveThread != null) receiveThread.Abort();
        if (client != null) client.Close();
    }
}