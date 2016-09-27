using System;

namespace model.Interfaces
{
    public interface ISlot : IEntity
    {
        int Number { get; set; }

        string SlotMessage { get; set; }
        string ContentType { get; set; }
        byte[] Content { get; set; }

        DateTime EarliestDateOfAllowedOpeningTime { get; set; }
        Nullable<DateTime> Opened { get; set; }
    }
}