namespace Exam.Data
{
    using Exam.Data.Repositories;
    using Exam.Models;

    public interface IBullsAndCowsData
    {
        IRepository<Game> Games { get; }

        IRepository<Guess> Guesses { get; }

        IRepository<Notification> Notifications { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
