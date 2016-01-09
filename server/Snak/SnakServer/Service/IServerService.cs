using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SnakServer.Service
{
    /// <summary>
    /// A WCF endpoint interface with all available methods.
    /// </summary>
    [ServiceContract]
    interface IServerService
    {

        /// <summary>
        /// A test method for WCF functionality
        /// </summary>
        /// <param name="input">An integer to be returned incremented</param>
        /// <returns>The parameter incremented by one</returns>
        [OperationContract]
        int GetSampleData(int input);

        /// <summary>
        /// Save a highscore from the game to the server app
        /// </summary>
        /// <param name="playerName">A player's name</param>
        /// <param name="levelName">The level on which the score was achieved</param>
        /// <param name="score">The integer value for the score</param>
        [OperationContract]
        void SaveHighscore(String playerName, String levelName, int score);

        /// <summary>
        /// Returns a List of Highscores for a specific level.
        /// </summary>
        /// <param name="levelName">The level name</param>
        /// <returns>A List of Highscore Objects</returns>
        [OperationContract]
        WcfEntities.Highscores getHighscoresForLevel(String levelName);

        /// <summary>
        /// Returns a List of Highscores achieved by a player
        /// </summary>
        /// <param name="playerName">The name of the player</param>
        /// <returns>A List of Highscore objects</returns>
        [OperationContract]
        WcfEntities.Highscores getHighscoresForPlayer(String playerName);
    }
}
