using System;
using Seq.Apps;
using Seq.Apps.LogEvents;

namespace Seq.App.Trello
{
    [SeqApp("Trello",Description="Creates a Trello card out of the event details.")]
    public class TrelloReactor : Reactor, ISubscribeTo<LogEventData>
    {

        [SeqAppSetting(
            DisplayName = "Trello API Key",
            HelpText = "Available at https://trello.com/app-key")]
        public string TrelloApiKey { get; set; }

        [SeqAppSetting(
            DisplayName = "Trello API Secret",
            InputType = SettingInputType.Password)]
        public string TrelloApiSecret { get; set; }

        [SeqAppSetting(
            DisplayName = "Board Name",
            HelpText = "Name of the board card will be added to.")]
        public string BoardName { get; set; }

        [SeqAppSetting(
          DisplayName = "List Name",
          HelpText = "Name of the list card should be added to.")]
        public string ListName { get; set; }

        public void On(Event<LogEventData> evt)
        {
            throw new NotImplementedException();
        }

    }
}
