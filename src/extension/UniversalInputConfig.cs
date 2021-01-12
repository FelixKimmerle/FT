namespace FT.src.extension
{
    public class UniversalInputConfig
    {
        public InputMode Mode { private set; get; }
        public byte Digital { private set; get; }

        public UniversalInputConfig()
        {
            Mode = InputMode.Resistence;
            Digital = 1;
        }

        public UniversalInputConfig(InputMode Mode, byte Digital)
        {
            this.Mode = Mode;
            this.Digital = Digital;
        }

        public byte GetByte()
        {
            byte digitalMask = (Digital != 0) ? (byte)0x80 : (byte)0x00;
            return (byte)(digitalMask | (byte)Mode);
        }
    }
}