using System;
using System.Buffers;
using System.Linq;
using Google.Protobuf;
using SystemMemorySerializer.Messages;

namespace SystemMemorySerializer;

public class RecordSerializer : Serializer
{
    public const string RecordAManifest = "RA";
    public const string RecordBManifest = "RB";

    public override int Identifier => 101;

    public override string ToBinary(IBufferWriter<byte> bufferWriter, object obj)
    {
        switch (obj)
        {
            case RecordA recordA:
            {
                WriteMessage(ToProto(recordA), bufferWriter);
                return RecordAManifest;
            }
            case RecordB recordB:
            {
                WriteMessage(ToProto(recordB), bufferWriter);
                return RecordBManifest;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(obj), "unsupported type");
        }
    }

    public override object FromBinary(IMemoryOwner<byte> memory, string manifest)
    {
        switch (manifest)
        {
            case RecordAManifest:
            {
                return RecordAFromProto(memory);
            }
            case RecordBManifest:
            {
                return RecordBFromProto(memory);
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(manifest), $"unsupported manifest [{manifest}]");
        }
    }

    private static Messages.Proto.RecordA ToProto(RecordA a)
    {
        var pRecordA = new Messages.Proto.RecordA()
        {
            Id = a.Id,
            Timestamp = a.Timestamp
        };
        
        return pRecordA;
    }
    
    private static Messages.Proto.RecordB ToProto(RecordB a)
    {
        var pRecordB = new Messages.Proto.RecordB()
        {
            Id = a.Id
        };
        pRecordB.RecordAs.AddRange(a.RecordAs.Select(ToProto));

        return pRecordB;
    }

    private static RecordA RecordAFromProto(IMemoryOwner<byte> buffer)
    {
        var pRecordA = Messages.Proto.RecordA.Parser.ParseFrom(buffer.Memory.Span);
        return FromProto(pRecordA);
    }

    private static RecordA FromProto(Messages.Proto.RecordA pRecordA)
    {
        return new RecordA(pRecordA.Id, pRecordA.Timestamp);
    }

    private static RecordB FromProto(Messages.Proto.RecordB pRecordB)
    {
        return new RecordB(pRecordB.Id, pRecordB.RecordAs.Select(FromProto).ToList());
    }

    private static RecordB RecordBFromProto(IMemoryOwner<byte> buffer)
    {
        return FromProto(Messages.Proto.RecordB.Parser.ParseFrom(buffer.Memory.Span));
    }

    private static void WriteMessage(IMessage msg, IBufferWriter<byte> bufferWriter)
    {
        var size = msg.CalculateSize();
        msg.WriteTo(bufferWriter);
        bufferWriter.Advance(size);
    }
}