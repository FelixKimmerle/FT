using System;
using System.Collections;
using System.Collections.Generic;

namespace FT.src.packets
{
    public abstract class Packet
    {
        protected bool SendPayload = true;
        protected Header header = new Header();

        protected virtual List<byte> GeneratePayload() { return new List<byte>(); }

        public ushort SessionId
        {
            set
            {
                header.SessionId = value;
            }
        }

        public ushort TransactionId
        {
            set
            {
                header.TrancactionId = value;
            }
        }

        public List<byte> GetBytes()
        {
            List<byte> bytes = new List<byte>();
            List<byte> payload = GeneratePayload();
            bytes.Add(0x02);
            bytes.Add(0x55);

            short payloadLength = (short)payload.Count;
            header.Length = (short)(20 + payloadLength);

            bytes.AddRange(header.GetBytes());

            if (SendPayload)
            {
                bytes.AddRange(payload);
            }

            bytes.AddRange(Utils.GetBytes(CalculateCRC(bytes), true));
            bytes.Add(0x03);

            return bytes;
        }

        private short CalculateCRC(List<byte> data)
        {
            short crc = 0;

            for (int i = 2; i < data.Count; i++)
            {
                crc += data[i];
            }

            crc = (short)((crc ^ -1) + 1);
            return crc;
        }

        public override string ToString()
        {
            List<byte> data = GetBytes();
            string str = "";
            foreach (byte b in data)
            {
                BitArray bitArray = new BitArray(b);
                str += (int)b + " ";
            }
            return str;
        }
    }
}