using UnityEngine;
using System.Collections.Generic;

public class LabviewToGraphBridge : MonoBehaviour
{
    public UDPDataReceiver receiver; // Arraste o UDP_Manager aqui
    public VoltageGraph voltageGraph; // Arraste o objeto do gráfico amarelo aqui
    private List<float> dynamicPoints = new List<float>();

    void Update()
    {
        // Se chegou dado novo do LabVIEW
        if (receiver != null && receiver.dataUpdate)
        {
            if (float.TryParse(receiver.lastReceivedPacket, out float newValue))
            {
                dynamicPoints.Add(newValue);

                // Limita a 30 pontos para o gráfico "correr"
                if (dynamicPoints.Count > 30) dynamicPoints.RemoveAt(0);

                // Força o gráfico amarelo a redesenhar com o dado do LabVIEW
                // Usamos 1.0f como Y Max porque sua senoide no LabVIEW vai de -1 a 1
                voltageGraph.DisplayVoltageGraph(dynamicPoints, 1.0f, "Bancada Real");
            }
            receiver.dataUpdate = false; 
        }
    }
}