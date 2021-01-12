namespace FT.src.extension
{
    public class Extension
    {
        public const int MOTORS = 4;
        public const int UNIVERSALINPUTS = 8;
        public string Name { set; get; }
        public Version Version { set; get; }
        public string MacAdress { set; get; }

        public Config config = new Config();
        public Output output = new Output();
        public Input input = new Input();

        public uint TA_INDEX { private set; get; }

        public Extension(byte index)
        {
            this.TA_INDEX = index;
        }

    }
}