namespace Exam.RESTApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Exam.Data;
    using Exam.Models;
    using Exam.RESTApi.Models;
    using System.Web;

    [Authorize]
    public class GamesController : BaseApiController
    {
        private const int PageSize = 10;
        private Random rnd;
        private const int BullCountToWin = 4;
        private const string WinMessageTemplate = "You beat {0} in game \"New hope\"";
        private const string YouJoinedGameMsgTemplate = "You joined game \"{0}\"";
        private const string LossMessageTemplate = "{0} beat you in game \"{1}\"";
        private const string ItsYourTurnInGame = "It is your turn in game \"{0} by {1}\"";
        private const string JoinedYourGame = "{0} joined your game \"{1}\"";

        public GamesController(IBullsAndCowsData data)
            : base(data)
        {
            this.rnd = new Random();
        }

        [HttpPost]
        public IHttpActionResult CreateGame(GameCreationDataModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.BadRequest(this.ModelState);
            }

            if (this.IsNumberValid(model.Number) == false)
            {
                return this.BadRequest("Invalid number");
            }

            var currentUser = this.User.Identity;

            var newGame = new Game()
            {
                Name = model.Name,
                RedNumber = model.Number,
                RedPlayerId = currentUser.GetUserId(),
                DateCreated = DateTime.Now,
                GameState = GameState.WaitingForOpponent,
            };

            this.data.Games.Add(newGame);
            this.data.SaveChanges();

            model.Id = newGame.Id;
            model.Red = currentUser.GetUserName();
            model.DateCreated = newGame.DateCreated;

            var location = HttpContext.Current.Request.Url.AbsolutePath;
            var gameLocation = string.Format
                ("{0}/{1}", location, newGame.Id);

            return
                this.Created<GameCreationDataModel>(gameLocation, model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult AllGames()
        {
            var userIsLogged = this.User.Identity.IsAuthenticated;
            IQueryable<OutputGameDataModelShort> result = null;

            if (userIsLogged)
            {
                result = this.GetAllGamesLoggedIn();
            }
            else
            {
                result = this.GetAllGamesNotLoggedIn();
            }

            return this.Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult AllGamesPaged(int page)
        {
            if (page < 0)
            {
                return this.BadRequest("Page number should be non-negative.");
            }

            var userIsLogged = this.User.Identity.IsAuthenticated;
            IQueryable<OutputGameDataModelShort> result = null;

            if (userIsLogged)
            {
                result = this.GetPagedGamesWhenLoggedIn(page);
            }
            else
            {
                result = this.GetPagedGamesWhenNotLoggedIn(page);
            }

            return this.Ok(result);
        }

        [HttpGet]
        public IHttpActionResult GamesById(int id)
        {
            // check if player participates in game
            var game = this.data.Games.Find(id);
            if (game == null)
            {
                return this.BadRequest("Invalid gamei id.");
            }

            var currUserId = this.User.Identity.GetUserId();

            if (game.RedPlayerId != currUserId &&
                game.BluePlayerId != currUserId)
            {
                return this.BadRequest("You can only view your games.");
            }

            if (game.GameState != GameState.BlueInTurn &&
                game.GameState != GameState.RedInTurn)
            {
                return this.BadRequest("You can only view ongoing games.");
            }
            
            //// lazy way of converting Game to GameOutputModelDetailed
            //var gameAsModel = this.data.Games.All()
            //    .Where(g => g.Id == game.Id)
            //    .Select(g => GameOutputModelDetailed.ToDetailedModel)
            //    .ToList().First();
            
            var gameAsModel = new GameOutputModelDetailed()
            {
                Id = game.Id,
                Name = game.Name,
                DateCreated = game.DateCreated,
                Red = game.RedPlayer.UserName,
                Blue = game.BluePlayer.UserName,
                GameState = game.GameState.ToString(),
                YourNumber = this.GetUserNumberById(game, currUserId),
                YourColor = this.GetUserColorById(game, currUserId),
                YourGuesses = this.data.Guesses.All()
                    .Where(g => g.UserId == currUserId)
                    .Select(GuessOutputDataModel.ToDataModel).ToList(),
            };


            return this.Ok(gameAsModel);
        }

        [HttpPut]
        public IHttpActionResult JoinGame(int id, InputNumberDataModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.BadRequest(this.ModelState);
            }

            var gameToJoin = this.data.Games.Find(id);

            if (gameToJoin == null)
            {
                return this.BadRequest("Invalid game id.");
            }

            var number = model.Number;
            var numberIsvalid = this.IsNumberValid(number);

            if (numberIsvalid == false)
            {
                return this.BadRequest("Invalid number");
            }

            var currentPlayerId = this.User.Identity.GetUserId();

            if (gameToJoin.RedPlayerId == currentPlayerId ||
                gameToJoin.BluePlayerId == currentPlayerId) // maybe not necessary if blue is the same player
            {
                return this.BadRequest("You cannot join your own game.");
            }

            // Client should check every few seconds 
            // for new alerts for their player and get this alert
            this.NotifyPlayer(gameToJoin, string.Format(JoinedYourGame, this.User.Identity.Name, gameToJoin.Name), gameToJoin.RedPlayerId);

            gameToJoin.GameState =
                this.rnd.Next(0, 101) > 50 ? GameState.RedInTurn : GameState.BlueInTurn;
            gameToJoin.BluePlayerId = currentPlayerId;
            gameToJoin.BlueNumber = model.Number;
            this.data.SaveChanges();

            this.NotifyPlayer(
                gameToJoin,
                string.Format(ItsYourTurnInGame, gameToJoin.Name, gameToJoin.RedPlayer.UserName),
                this.GetPlayerIdByTurn(gameToJoin));

            return this.Ok(new
                {
                    Result = string.Format(YouJoinedGameMsgTemplate, gameToJoin.Name),
                });
        }

        [HttpPost]
        [ActionName("MakeGuess")]
        public IHttpActionResult MakeGuess(int id, GuessInputDataModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.BadRequest(this.ModelState);
            }

            // TODO: Make validations in separate project or method
            var guessNumberIsValid = this.IsNumberValid(model.Number);
            if (guessNumberIsValid == false)
            {
                return this.BadRequest("Invalid guess number");
            }

            var game = this.data.Games.Find(id);
            if (game == null)
            {
                return this.BadRequest("Invalid game id.");
            }

            // Check if game is running
            if (game.GameState != GameState.RedInTurn &&
                game.GameState != GameState.BlueInTurn)
            {
                return this.BadRequest("This game is not running.");
            }

            // Check if user is in the game
            var currUserId = this.User.Identity.GetUserId();
            if (game.RedPlayerId != currUserId &&
                game.BluePlayerId != currUserId)
            {
                return this.BadRequest("You are not part of this game.");
            }

            // and its his turn
            var idOfPlayerToAct = this.GetPlayerIdByTurn(game);
            if (idOfPlayerToAct != currUserId)
            {
                return this.BadRequest("It is not your turn.");
            }

            // count bulls and cows
            var opponentNumber = this.GetOpponentsNumber(game);
            var bullCount = this.CountBulls(opponentNumber, model.Number);
            var cowCount = this.CountCows(opponentNumber, model.Number);

            var newGuess = new Guess()
            {
                Number = model.Number,
                DateMade = DateTime.Now,
                GameId = game.Id,
                UserId = this.User.Identity.GetUserId(),
                BullsCount = bullCount,
                CowsCount = cowCount,
            };

            this.data.Guesses.Add(newGuess);
            this.data.SaveChanges();

            if (bullCount == BullCountToWin)
            {
                this.GameWon(game, currUserId);
                return this.Ok();
            }

            game.GameState = game.GameState == GameState.RedInTurn ? GameState.BlueInTurn : GameState.RedInTurn;
            var idOfUserToPlay = this.GetPlayerIdByTurn(game);
            this.NotifyPlayer(
                game,
                string.Format(ItsYourTurnInGame, game.Name, game.RedPlayer.UserName),
                idOfUserToPlay);
            this.data.SaveChanges();

            var outputModel = new GuessOutputDataModel()
            {
                Id = newGuess.Id,
                UserId = newGuess.UserId,
                Username = this.User.Identity.Name,
                GameId = newGuess.GameId,
                DateMade = newGuess.DateMade,
                CowsCount = newGuess.CowsCount,
                BullsCount = newGuess.BullsCount,
                Number = model.Number,
            };

            return this.Ok(outputModel);
        }

        // Sort scores!

        // TODO: Move game logic outside controller

        private string GetUserNumberById(Game game, string id)
        {
            if (game.RedPlayerId == id)
            {
                return game.RedNumber;
            }

            return game.BlueNumber;
        }

        private string GetUserColorById(Game game, string id)
        {
            if (game.RedPlayerId == id)
            {
                return "red";
            }

            return "blue";
        }

        private void GameWon(Game game, string currUserId)
        {
            game.GameState = GameState.Finished;
            game.WinnerId = currUserId;

            var winningUser = this.GetGameUserById(game, currUserId);
            winningUser.WinsCount++;

            var loserId = this.GetLoserIdByWinnerId(game, game.WinnerId);
            var losingUser = this.GetGameUserById(game, loserId);
            losingUser.LossesCount++;

            // notify winner
            this.NotifyPlayer(
                game,
                string.Format(WinMessageTemplate, winningUser.UserName, game.Name),
                currUserId);

            // notify loser
            this.NotifyPlayer(
                game,
                string.Format(LossMessageTemplate, winningUser.UserName, game.Name),
                this.GetLoserIdByWinnerId(game, game.WinnerId));
        }

        private string GetLoserIdByWinnerId(Game game, string winnerId)
        {
            if (game.RedPlayerId == winnerId)
            {
                return game.BluePlayer.Id;
            }

            if (game.BluePlayerId == winnerId)
            {
                return game.RedPlayer.Id;
            }

            throw new ArgumentException();
        }

        private User GetGameUserById(Game game, string id)
        {
            if (game.RedPlayerId == id)
            {
                return game.RedPlayer;
            }

            if (game.BluePlayerId == id)
            {
                return game.BluePlayer;
            }

            throw new ArgumentException();
        }

        private string GetOpponentsNumber(Game game)
        {
            if (game.GameState == GameState.BlueInTurn)
            {
                return game.RedNumber;
            }

            if (game.GameState == GameState.RedInTurn)
            {
                return game.BlueNumber;
            }

            throw new ArgumentException();
        }

        private int CountBulls(string playerNum, string guessNum)
        {
            int bullCount = 0;

            for (int i = 0; i < playerNum.Length; i++)
            {
                if (playerNum[i] == guessNum[i])
                {
                    bullCount++;
                }
            }

            return bullCount;
        }

        private int CountCows(string playerNum, string guessNum)
        {
            int cowCount = 0;

            for (int i = 0; i < guessNum.Length; i++)
            {
                var digit = guessNum[i];

                if (playerNum.Contains(digit) &&
                    guessNum[i] != playerNum[i])
                {
                    cowCount++;
                }
            }

            return cowCount;
        }

        private bool IsNumberValid(string number)
        {
            var hasAnythingButDigits = false;
            var uniqueDigits = new HashSet<char>();

            foreach (var digit in number)
            {
                uniqueDigits.Add(digit);

                if (char.IsDigit(digit) == false)
                {
                    hasAnythingButDigits = true;
                    break;
                }
            }

            var hasRepeatingDigits = uniqueDigits.Count != 4;

            if (string.IsNullOrWhiteSpace(number) ||
                number.Length != 4 ||
                hasAnythingButDigits ||
                hasRepeatingDigits)
            {
                return false;
            }

            return true;
        }

        private string GetPlayerIdByTurn(Game game)
        {
            var state = game.GameState;

            if (state == GameState.BlueInTurn)
            {
                return game.BluePlayerId;
            }
            else if (state == GameState.RedInTurn)
            {
                return game.RedPlayerId;
            }

            throw new ArgumentException();
        }

        private void NotifyPlayer(Game game, string message, string userId)
        {
            var alert = new Notification()
            {
                UserId = userId,
                Message = message,
                DateCreated = DateTime.Now,
                State = false,
                GameId = game.Id,
            };

            this.data.Notifications.Add(alert);
            this.data.SaveChanges();
        }

        private IQueryable<OutputGameDataModelShort> GetAllGamesNotLoggedIn()
        {
            var allGamesAsModels = this.GetGamesWithOnePlayerAsModels();
            var sortedGameModels = this.SortGames(allGamesAsModels);

            return sortedGameModels;
        }

        private IQueryable<OutputGameDataModelShort> GetAllGamesLoggedIn()
        {
            var currPlayerId = this.User.Identity.GetUserId();

            var games = this.data.Games.All()
                .Where(g => (g.RedPlayerId == currPlayerId ||
                             g.BluePlayerId == currPlayerId) ||
                             g.GameState == GameState.WaitingForOpponent)
                .Select(OutputGameDataModelShort.ToDataModel);

            var result = this.SortGames(games);

            return result;
        }

        private IQueryable<OutputGameDataModelShort> GetPagedGamesWhenLoggedIn(int page)
        {
            var games = this.GetAllGamesLoggedIn();
            var pagedGames = games
                .Skip(PageSize * page)
                .Take(PageSize);

            return pagedGames;
        }

        private IQueryable<OutputGameDataModelShort> GetPagedGamesWhenNotLoggedIn(int page)
        {
            var pagedGames = this.GetAllGamesNotLoggedIn()
                .Skip(PageSize * page)
                .Take(PageSize);

            return pagedGames;
        }

        private IQueryable<OutputGameDataModelShort> SortGames(IQueryable<OutputGameDataModelShort> games)
        {
            var result = games.OrderBy(g => g.GameState.ToString())
                .ThenBy(g => g.Name)
                .ThenByDescending(g => g.DateCreated)
                .ThenBy(g => g.Red);

            return result;
        }

        private IQueryable<OutputGameDataModelShort> GetGamesWithOnePlayerAsModels()
        {
            var allAvailableGamesAsModels =
                   this.data.Games.All()
                   .Where(g => g.GameState == GameState.WaitingForOpponent)
                   .Select(OutputGameDataModelShort.ToDataModel);

            return allAvailableGamesAsModels;
        }
    }
}
