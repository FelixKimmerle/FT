using System.Collections.Generic;
using FT.src.extension;

namespace FT.src.packets
{
    public class RequestInfoPacket : Packet
    {
        private readonly List<Extension> extensions;
        public RequestInfoPacket(List<Extension> extensions)
        {
            this.extensions = extensions;
            header.CommandCode = 0x06;
            header.NumPayload = extensions.Count;
        }

        protected override List<byte> GeneratePayload()
        {
            List<byte> bytes = new List<byte>();

            foreach (Extension extension in extensions)
            {
                bytes.AddRange(Utils.GetBytes(extension.TA_INDEX));
            }

            return bytes;
        }

    }
}