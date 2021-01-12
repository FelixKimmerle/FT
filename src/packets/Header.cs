using System;
using System.Collections.Generic;

namespace FT.src.packets
{
    public class Header
    {
        public int From {get; set;} = 0x02;
        public int To {get; set;} = 0x01;
        public ushort TrancactionId {get; set;} = 0;
        public ushort SessionId {get; set;}
        public int CommandCode {get; set;} = 0;
        public int NumPayload {get; set;} = 1;
        public short Length {get; set;} = 27;

        public void Load(List<byte> bytes)
        {
            Length = BitConverter.ToInt16(Utils.GetByteArray(bytes, 2, 2, true));
            From = BitConverter.ToInt32(Utils.GetByteArray(bytes, 4, 4));
            To = BitConverter.ToInt32(Utils.GetByteArray(bytes, 8, 4));
            TrancactionId = BitConverter.ToUInt16(Utils.GetByteArray(bytes, 12));
            SessionId = BitConverter.ToUInt16(Utils.GetByteArray(bytes, 14));
            CommandCode = BitConverter.ToInt32(Utils.GetByteArray(bytes, 16, 4));
            if(bytes.Count > 27)
            {
                NumPayload = BitConverter.ToInt32(Utils.GetByteArray(bytes, 20, 4));
            }
        }

        public List<byte> GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(Utils.GetBytes(Length, true));

            bytes.AddRange(Utils.GetBytes(From));
            bytes.AddRange(Utils.GetBytes(To));
            bytes.AddRange(Utils.GetBytes(TrancactionId));
            bytes.AddRange(Utils.GetBytes(SessionId));
            bytes.AddRange(Utils.GetBytes(CommandCode));
            bytes.AddRange(Utils.GetBytes(NumPayload));

            return bytes;
        }

    }
}