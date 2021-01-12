namespace FT.src.packets
{
    public class EchoPacket : Packet
    {
        public EchoPacket()
        {
            header.CommandCode = 0x01;
            SendPayload = false;
        }
    }
}