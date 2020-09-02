using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.IO;
using System;
public class CreateFileWithInfoTest : MonoBehaviour
{
    [SerializeField] private bool OpenADoor;
    SerialPort port;
    
    // Start is called before the first frame update
    void Start()
    {
        port = new SerialPort();
        port.PortName = "COM3";
        port.BaudRate = 115200;
        port.Parity = Parity.None;
        port.StopBits = StopBits.One;
        port.DataBits = 8;
        port.Handshake = Handshake.None;
        port.RtsEnable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (OpenADoor)
        {
            if (!port.IsOpen)
                port.Open();
            using (StreamWriter outputfile = new StreamWriter("D:/Programs/UnityProject/InformationFromPorts/Assets/Documents/Test.txt",true))
            {
                outputfile.WriteLine(port.ReadLine());
            }
        }
        else
        {
            if (port.IsOpen)
            {
                port.Close();
            }
        }
    }
    #region GETER and SETER
    public bool GetDoorState()
    {
        return OpenADoor;
    }

    public void SetDoorState(bool b)
    {
        OpenADoor = b;
    }
    #endregion
}
