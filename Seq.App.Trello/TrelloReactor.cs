using System;
using System.Linq;
using Seq.Apps;
using Seq.Apps.LogEvents;
using TrelloNet;

namespace Seq.App.Trello
{
    [SeqApp("Trello", Description = "Creates a Trello card out of the event details.")]
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
            try
            {
                ITrello trello = new TrelloNet.Trello(TrelloApiKey);

                trello.Authorize(TrelloApiSecret);

                var board = trello.Boards.Search(BoardName).FirstOrDefault();
                if (board == null)
                    throw new ArgumentException(string.Format("Could not find Trello board named '{0}'", BoardName));

                var list =trello.Lists.ForBoard(board) .FirstOrDefault(x => x.Name.Equals(ListName, StringComparison.OrdinalIgnoreCase));

                if (list == null)
                    throw new ArgumentException(string.Format("Could not find Trello list named '{0}'", BoardName));

                // TODO parse the details out better
                trello.Cards.Add(new NewCard(evt.Data.RenderedMessage, list));

            }
            catch (Exception ex)
            {
                Log.Warning("Failed to create new Trello card.", ex);
            }

        }

    }
}
