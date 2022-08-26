using System.Collections.Generic;

namespace SystemMemorySerializer.Messages;

public record RecordA(string Id, long Timestamp);

public record RecordB(string Id, IReadOnlyList<RecordA> RecordAs);