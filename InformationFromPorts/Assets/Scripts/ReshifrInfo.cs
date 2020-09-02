using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
public class ReshifrInfo : MonoBehaviour
{
    public bool OpenADoor;
    SerialPort port;
    // Start is called before the first frame update
    void Start()
    {
        port = new SerialPort();
        port.PortName = "COM3";
        port.BaudRate = 9600;
        port.Parity = Parity.None;
        port.StopBits = StopBits.One;
        port.DataBits = 8;
        port.Handshake = Handshake.None;
        port.RtsEnable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(OpenADoor)
        {
            if(!port.IsOpen)
                port.Open();

            Debug.Log(port.ReadLine());
        }
        else
        {
            if(port.IsOpen)
            {
                port.Close();
            }
        }
    }
}
