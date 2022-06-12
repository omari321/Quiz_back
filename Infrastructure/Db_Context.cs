using Infrastructure.Entities.Answers;
using Infrastructure.Entities.Questions;
using Infrastructure.Entities.Quiz;
using Infrastructure.Entities.QuizQuotes;
using Infrastructure.Entities.QuizResults;
using Infrastructure.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Db_Context : DbContext
    {
        public DbSet<UserEntity> userEntities { get; set; }
        public DbSet<QuizEntity> quizEntities { get; set; }
        public DbSet<QuizQuotesEntity> quizQuotesEntities { get; set; }
        public DbSet<QuotesEntity> quotesEntities { get; set; }
        public DbSet<AnswersEntity> answersEntities { get; set; }
        public DbSet<QuizResultsEntity> quizResultsEntities { get; set; } 
        public Db_Context(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
