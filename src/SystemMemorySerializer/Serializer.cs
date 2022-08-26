using System.Buffers;

namespace SystemMemorySerializer
{
    public abstract class Serializer
    {
        protected Serializer(short identifier)
        {
            Identifier = identifier;
        }

        public virtual short Identifier { get; }
        
        public abstract bool IncludeManifest { get; }

        public abstract IMemoryOwner<byte> ToBinary(object obj);

        public abstract string ToManifest(object obj);

        public abstract object FromBinary(IMemoryOwner<byte> memory, string manifest);
    }
}
