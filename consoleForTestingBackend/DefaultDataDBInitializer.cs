using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using model.POCO;

namespace consoleForTestingBackend
{
    public class DefaultDataDBInitializer : DropCreateDatabaseAlways<AdventCalendarContext>
    {
        protected override void Seed(AdventCalendarContext context)
        {
            var calendar = new model.POCO.Calendar {Active = true};

            IEnumerable<model.POCO.Slot> slots;

            var ci = CultureInfo.InvariantCulture;
            var active = true;
            var opened = false;


            context.Configuration.AutoDetectChangesEnabled = false;

            context.Calendars.Add(calendar);
            /*
            slots = new List<model.POCO.Slot>()
            {
                new Slot {Number = 1, SlotMessage = "Bakom gardin [TV-rum, vänster väggfäste]",                                 ImageURL = "1.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 1)},
                new Slot {Number = 2, SlotMessage = "Atikas cykelhjälm [Grovkök, nänster spegelskåp]",                          ImageURL = "2.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 2)},
                new Slot {Number = 3, SlotMessage = "Inomhus ovanför dörren [Lekstuga]",                                        ImageURL = "3.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 3)},
                new Slot {Number = 4, SlotMessage = "Vaskskåp [Grovkök]",                                                       ImageURL = "4.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 4)},
                new Slot {Number = 5, SlotMessage = "Vinranka [Växthus]",                                                       ImageURL = "5.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 5)},
                new Slot {Number = 6, SlotMessage = "Hängandes under näst nedersta klädkorgen [Atikas rum, vänster garderob]",  ImageURL = "6.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 6)},
                new Slot {Number = 7, SlotMessage = "Badrumsskåp [Lilla toaletten]",                                            ImageURL = "7.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 7)},
                new Slot {Number = 8, SlotMessage = "På krok vid verktygstavla [Garaget]",                                      ImageURL = "8.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 8)},
                new Slot {Number = 9, SlotMessage = "Bakom brandsläckare [Kök]",                                                ImageURL = "9.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 9)},
                new Slot {Number = 10,SlotMessage = "Bröllopsklänning [Grovkök, vänster spegelskåp]",                           ImageURL = "10.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 10)},
                new Slot {Number = 11,SlotMessage = "Inomhus, på sidan vid porten [Garage]",                                    ImageURL = "11.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 11)},
                new Slot {Number = 12,SlotMessage = "I klänning [Atikas rum, höger garderob]",                                  ImageURL = "12.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 12)},
                new Slot {Number = 13,SlotMessage = "Under orange hjälm [Garage]",                                              ImageURL = "13.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 13)},
                new Slot {Number = 14,SlotMessage = "Porslinsvas med konstblommor [Uterummet]",                                 ImageURL = "14.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 14)},
                new Slot {Number = 15,SlotMessage = "Bakom gardin [Kök, höger väggfäste]",                                      ImageURL = "15.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 15)},
                new Slot {Number = 16,SlotMessage = "Vattentillförselkran på toalettens sida [Lilla toaletten]",                ImageURL = "16.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 16)},
                new Slot {Number = 17,SlotMessage = "Hans mörkblå kostym [Grovkök, höger spegelskåp]",                          ImageURL = "17.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 17)},
                new Slot {Number = 18,SlotMessage = "Gamla Hörby-antennen [Vinden]",                                            ImageURL = "18.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 18)},
                new Slot {Number = 19,SlotMessage = "Medaljtavla [Hans rum]",                                                   ImageURL = "19.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 19)},
                new Slot {Number = 20,SlotMessage = "På en utstickande spik från en av takstolarna [Vinden]",                   ImageURL = "20.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 20)},
                new Slot {Number = 21,SlotMessage = "Vägguttag bakom piano [Vardagsrum]",                                       ImageURL = "21.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 21)},
                new Slot {Number = 22,SlotMessage = "Bland skruvtvingar [Garage, verktygstavla]",                               ImageURL = "22.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 22)},
                new Slot {Number = 23,SlotMessage = "Under vaskskåp [Lilla toaletten]",                                         ImageURL = "23.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 23)},
                new Slot {Number = 24,SlotMessage = "Inuti (grå) golvlampa [Vardagsrum]",                                       ImageURL = "24.jpg", Active = active, Opened = opened, EarliestDateOfAllowedOpeningTime = new DateTime(2015, 12, 24)}
            };

            context.Slots.AddRange(slots);
            */
            context.SaveChanges();
        }
    }
}