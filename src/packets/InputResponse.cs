using System;
using System.Collections.Generic;
using FT.src.extension;

namespace FT.src.packets
{
    public class InputResponse : Response
    {
        private List<Extension> extensions;

        public InputResponse(List<Extension> extensions)
        {
            this.extensions = extensions;
        }

        public override int GetLength()
        {
            return 52 * extensions.Count + 27;
        }

        protected override void LoadPayload(List<byte> bytes)
        {
            int offset = 0;

            for (int e = 0; e < header.NumPayload; e++)
            {
                int transferarea = BitConverter.ToInt32(Utils.GetByteArray(bytes, offset, 4));
                offset += 4;
                Extension extension = extensions[transferarea];
                for (int i = 0; i < Extension.UNIVERSALINPUTS; i++)
                {
                    ushort value = BitConverter.ToUInt16(Utils.GetByteArray(bytes, offset));
                    extension.input.SetUniversalInput(i, value);
                    offset += 2;
                }

                for (int i = 0; i < Extension.MOTORS; i++)
                {
                    byte value = bytes[offset];
                    extension.input.SetCounterInput(i, value);
                    offset += 1;
                }

                for (int i = 0; i < Extension.MOTORS; i++)
                {
                    ushort value = BitConverter.ToUInt16(Utils.GetByteArray(bytes, offset));
                    extension.input.SetCounterValue(i, value);
                    offset += 2;
                }

                ushort leftValue = BitConverter.ToUInt16(Utils.GetByteArray(bytes, offset));
                extension.input.SetDisplayButtonLeft(leftValue);
                offset += 2;
                ushort rightValue = BitConverter.ToUInt16(Utils.GetByteArray(bytes, offset));
                extension.input.SetDisplayButtonRight(rightValue);
                offset += 2;
                //System.Console.WriteLine("TA: " + transferarea);

                for (int i = 0; i < Extension.MOTORS; i++)
                {
                    ushort reset = BitConverter.ToUInt16(Utils.GetByteArray(bytes, offset));
                    extension.input.SetResetCounter(i, reset);
                    //System.Console.WriteLine("C" + i + ": " + reset);
                    offset += 2;
                }

                for (int i = 0; i < Extension.MOTORS; i++)
                {
                    ushort reached = BitConverter.ToUInt16(Utils.GetByteArray(bytes, offset));
                    extension.input.SetMotorReachedDestination(i, reached);
                    //System.Console.WriteLine("M" + i + ": " + reached);
                    offset += 2;
                }
                //Console.WriteLine(offset);
            }
        }

    }
}