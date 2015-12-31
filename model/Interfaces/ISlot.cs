using System;

namespace model.Interfaces
{
    public interface ISlot : IEntity
    {
        int Number { get; set; }
        bool Opened { get; set; }

        DateTime EarliestDateOfAllowedOpeningTime { get; set; }
        string ImageURL { get; set; }
        string SlotMessage { get; set; }
    }
}