using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using FT.src.extension;
using FT.src.packets;

namespace FT.src
{
    public class FtConnection
    {
        private readonly SerialPort serialPort;
        private ushort sessionId = 0;
        private ushort transactionId = 0;
        public readonly List<Extension> extensions = new List<Extension>();

        public FtConnection(string comPort, int numExtensions = 1)
        {
            serialPort = new SerialPort(comPort);
            serialPort.Open();
            //serialPort.ReadTimeout = 1000;

            EchoPacket packet = new EchoPacket();
            EchoResponse response = new EchoResponse();
            SendPacket(packet, response);
            if (!response.Success())
            {
                System.Console.WriteLine("Echo failed!");
            }

            for (int i = 0; i < numExtensions; i++)
            {
                extensions.Add(new Extension((byte)i));
            }

            ConfigPacket configPacket = new ConfigPacket(extensions);
            ConfigResponse configResponse = new ConfigResponse(extensions.Count);
            SendPacket(configPacket, configResponse);
            if (!configResponse.Success())
            {
                System.Console.WriteLine("Config failed!");
            }

            System.Console.WriteLine("Config sucess");

            RequestInfoPacket infoPacket = new RequestInfoPacket(extensions);
            RequestInfoResponse infoResponse = new RequestInfoResponse(extensions);
            SendPacket(infoPacket, infoResponse);

            System.Console.WriteLine(extensions[0].Name);
            System.Console.WriteLine(extensions[0].Version);
            System.Console.WriteLine(extensions[0].MacAdress);

            System.Console.WriteLine(extensions[1].Name);
            System.Console.WriteLine(extensions[1].Version);
            System.Console.WriteLine(extensions[1].MacAdress);

            //SetMotorSync(ExtensionIndex.Slave1, Motor.M1, Motor.M3, 512);
            //SetMotorDistance(ExtensionIndex.Slave1, Motor.M1, -512, 200);

            extensions[(int)1].output.MotorMasterValues[(int)Motor.M1] = (byte)(Motor.M3 + 1);
            SetMotorOutput(ExtensionIndex.Slave1, Motor.M3, 512);

            while (true)
            {

                Update();

                if (GetDigitalValue(ExtensionIndex.Local, UniversalInput.I8))
                {
                    SetMotorDistance(ExtensionIndex.Slave1, Motor.M1, -512, 200);
                }
                //Console.WriteLine(extensions[0].UniversalInputs[7]);
                //Console.WriteLine(extensions[0].UniversalInputs[5]);

                /*
                System.Console.WriteLine("-------------------");
                foreach (Extension extension in extensions)
                {
                    for (int i = 0; i < Extension.UNIVERSALINPUTS; i++)
                    {
                        System.Console.WriteLine(extension.UniversalInputs[i]);
                    }
                    System.Console.WriteLine("");

                }
                */

            }
        }

        public void SetPWMOutput(ExtensionIndex index, PWM pwm, short duty)
        {
            extensions[(int)index].output.PwmOutputValues[(int)pwm] = duty;
        }

        public void SetMotorOutput(ExtensionIndex index, Motor motor, short duty)
        {
            if (duty > 0)
            {
                extensions[(int)index].output.PwmOutputValues[2 * (int)motor] = duty;
                extensions[(int)index].output.PwmOutputValues[2 * (int)motor + 1] = 0;
            }
            else
            {
                extensions[(int)index].output.PwmOutputValues[2 * (int)motor] = 0;
                extensions[(int)index].output.PwmOutputValues[2 * (int)motor + 1] = (short)(duty * (-1));
            }
        }

        public void SetMotorDistance(ExtensionIndex index, Motor motor, short duty, ushort distance)
        {
            SetMotorOutput(index, motor, duty);
            extensions[(int)index].output.MotorDistanceValues[(int)motor] = distance;
            extensions[(int)index].output.MotorCommandId[(int)motor]++;
        }

        public void SetMotorSync(ExtensionIndex index, Motor motor, Motor other, short duty, ushort distance = 0)
        {
            SetMotorOutput(index, motor, duty);
            SetMotorOutput(index, other, duty);

            extensions[(int)index].output.MotorMasterValues[(int)motor] = (byte)(other + 1);
        }

        bool GetDigitalValue(ExtensionIndex index, UniversalInput input)
        {
            return extensions[(int)index].input.GetUniversalInput((int)input) != 0;
        }

        ushort GetAnalogValue(ExtensionIndex index, UniversalInput input)
        {
            return extensions[(int)index].input.GetUniversalInput((int)input);
        }

        private void Update()
        {
            OutputPacket outputPacket = new OutputPacket(extensions);
            InputResponse inputResponse = new InputResponse(extensions);

            SendPacket(outputPacket, inputResponse);

            foreach (Extension extension in extensions)
            {
                if (extension.input.Changed)
                {
                    System.Console.WriteLine(extension.input.GetMotorReachedDestination((int)Motor.M1));
                    extension.input.Changed = false;
                }
            }
        }

        private void SendPacket(Packet packet, Response response)
        {
            packet.SessionId = sessionId;
            packet.TransactionId = transactionId;
            byte[] data = packet.GetBytes().ToArray();
            //Console.WriteLine(BitConverter.ToString(data).Replace("-", " "));

            serialPort.Write(data, 0, data.Length);

            byte[] receivedData = new byte[response.GetLength()];

            while (serialPort.BytesToRead != receivedData.Length)
            {
                Thread.Sleep((int)(((float)data.Length / 64f) * 2f));
            }

            serialPort.Read(receivedData, 0, receivedData.Length);
            List<byte> bytes = new List<byte>();

            bytes.AddRange(receivedData);
            response.Load(bytes);
            sessionId = response.SessionId;
            transactionId = (ushort)(response.TransactionId + 1);
        }
    }
}