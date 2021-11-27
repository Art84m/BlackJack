using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blackjack
{
    public class GameIO
    {

        public static async void save(Game game, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<Game>(fs, game);
            }

        }

        public static Game load(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                ValueTask<Game> gameTask = JsonSerializer.DeserializeAsync<Game>(fs);
                return gameTask.Result;
            }
        }


    }
}
