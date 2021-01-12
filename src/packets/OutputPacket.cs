using System.Collections.Generic;
using FT.src.extension;

namespace FT.src.packets
{
    public class OutputPacket : Packet
    {
        private List<Extension> extensions;
        public OutputPacket(List<Extension> extensions)
        {
            this.extensions = extensions;
            header.CommandCode = 0x02;
            header.NumPayload = extensions.Count;
        }

        protected override List<byte> GeneratePayload()
        {
            List<byte> bytes = new List<byte>();

            foreach (Extension extension in extensions)
            {
                bytes.AddRange(Utils.GetBytes(extension.TA_INDEX));

                foreach (ushort value in extension.output.CounterResetCommandId)
                {
                    bytes.AddRange(Utils.GetBytes(value));
                }

                foreach (byte value in extension.output.MotorMasterValues)
                {
                    bytes.Add(value);
                }

                foreach (ushort value in extension.output.PwmOutputValues)
                {
                    bytes.AddRange(Utils.GetBytes(value));
                }

                foreach (ushort value in extension.output.MotorDistanceValues)
                {
                    bytes.AddRange(Utils.GetBytes(value));
                }

                foreach (ushort value in extension.output.MotorCommandId)
                {
                    bytes.AddRange(Utils.GetBytes(value));
                }
            }
            return bytes;
        }
    }
}