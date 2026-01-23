using UnityEngine;
using System.Collections.Generic;

public class BridgeLabviewUnity : MonoBehaviour
{
    public UDPDataReceiver receiver; // Arraste o UDP_Manager aqui no Inspector
    public VoltageGraph voltageGraph; // Arraste seu objeto de gráfico aqui
    private List<float> dataList = new List<float>();

    void Update()
    {
        if (receiver.dataUpdate)
        {
            // Tenta converter o texto do LabVIEW para número
            if (float.TryParse(receiver.lastReceivedPacket, out float val))
            {
                dataList.Add(val);

                // Mantém os últimos 30 pontos para o gráfico "correr"
                if (dataList.Count > 30) dataList.RemoveAt(0);

                // Atualiza o visual do seu gráfico existente
                voltageGraph.DisplayVoltageGraph(dataList, 220f, "Bancada NI");
            }
            receiver.dataUpdate = false; // Reseta o sinalizador
        }
    }
}