namespace FT.src.extension
{
    public class Version
    {
        public byte MajorVersion { private set; get; }
        public byte MinorVersion { private set; get; }
        public byte BuildVersion { private set; get; }

        public Version(byte majorVersion, byte minorVersion, byte buildVersion)
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            BuildVersion = buildVersion;
        }

        public override string ToString()
        {
            return MajorVersion + "." + MinorVersion + "." + BuildVersion;
        }
    }
}