using System.Collections.Generic;
using FT.src.extension;

namespace FT.src.packets
{
    public class ConfigPacket : Packet
    {
        private List<Extension> extensions;

        public ConfigPacket(List<Extension> extensions)
        {
            header.CommandCode = 0x05;
            this.extensions = extensions;
            header.NumPayload = extensions.Count;
            SendPayload = true;
        }

        protected override List<byte> GeneratePayload()
        {
            List<byte> bytes = new List<byte>();

            foreach (Extension extension in extensions)
            {
                bytes.AddRange(Utils.GetBytes(extension.TA_INDEX));

                for (int i = 0; i < Extension.MOTORS; i++)
                {
                    bytes.Add(extension.config.GetMotorConfig(i));
                }

                for (int i = 0; i < Extension.UNIVERSALINPUTS; i++)
                {
                    bytes.Add(extension.config.GetUniversalInputConfig(i).GetByte());
                }

                for (int i = 0; i < Extension.MOTORS; i++)
                {
                    bytes.Add(extension.config.GetCounterConfig(i));
                }

                for (int i = 0; i < 32; i++)
                {
                    bytes.Add(0);
                }
            }
            return bytes;
        }
    }
}