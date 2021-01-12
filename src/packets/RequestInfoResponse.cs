using System;
using System.Collections.Generic;
using System.Text;
using FT.src.extension;

namespace FT.src.packets
{

    public class RequestInfoResponse : Response
    {
        private readonly List<Extension> extensions;
        public RequestInfoResponse(List<Extension> extensions)
        {
            this.extensions = extensions;
        }

        private byte[] Trim(byte[] packet)
        {
            var i = packet.Length - 1;
            while (packet[i] == 0)
            {
                --i;
            }
            var temp = new byte[i + 1];
            Array.Copy(packet, temp, i + 1);
            return temp;
        }

        protected override void LoadPayload(List<byte> list)
        {
            int offset = 0;
            for (int i = 0; i < extensions.Count; i++)
            {
                int transferArea = BitConverter.ToInt32(Utils.GetByteArray(list, offset, 4));
                offset += 4;

                extensions[transferArea].Name = Encoding.ASCII.GetString(Trim(list.GetRange(offset, 17).ToArray()));
                offset += 17;
                extensions[transferArea].MacAdress = Encoding.ASCII.GetString(Trim(list.GetRange(offset, 18).ToArray()));
                offset += 18;
                offset += 1;
                uint ta_array_start_addr = BitConverter.ToUInt32(Utils.GetByteArray(list, offset, 4));
                offset += 4;
                uint pgm_area_start_addr = BitConverter.ToUInt32(Utils.GetByteArray(list, offset, 4));
                offset += 4;
                uint pgm_area_size = BitConverter.ToUInt32(Utils.GetByteArray(list, offset, 4));
                offset += 4;
                offset += 4;
                offset += 4;
                extensions[transferArea].Version = new extension.Version(list[offset], list[offset + 1], list[offset + 2]);
                offset += 4;
                offset += 4;

            }
        }

        public override int GetLength() { return 68 * extensions.Count + 27; }
    }


}