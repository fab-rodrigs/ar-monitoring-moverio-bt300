using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public class BridgeLabviewUnity : MonoBehaviour
{
    public UDPDataReceiver receiver;
    public VoltageGraph voltageGraph;

    // Lista para acumular os pontos em tempo real
    private List<float> voltageBuffer = new List<float>();
    [SerializeField] private int maxDataPoints = 30; // Quantidade de pontos visíveis na tela
    [SerializeField] private float maxVoltage = 10f; // Escala do eixo Y

    void Update()
    {
        if (receiver != null && receiver.dataUpdate)
        {
            // Trata a vírgula para ponto (ex: "0,587785" -> "0.587785")
            string rawData = receiver.lastReceivedPacket.Replace(",", ".");

            if (float.TryParse(rawData, NumberStyles.Any, CultureInfo.InvariantCulture, out float value))
            {
                // Adiciona o novo valor ao buffer
                voltageBuffer.Add(value);

                // Mantém apenas os últimos N pontos para criar o efeito de osciloscópio
                if (voltageBuffer.Count > maxDataPoints)
                {
                    voltageBuffer.RemoveAt(0);
                }

                // Atualiza o gráfico chamando a função com os 3 parâmetros exigidos
                if (voltageGraph != null)
                {
                    voltageGraph.DisplayVoltageGraph(voltageBuffer, maxVoltage, "Bancada WEG");
                }
            }

            receiver.dataUpdate = false;
        }
    }
}