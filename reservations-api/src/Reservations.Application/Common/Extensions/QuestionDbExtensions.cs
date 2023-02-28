using Microsoft.EntityFrameworkCore;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Extensions
{
    public static class QuestionDbExtensions
    {
        public static async Task<IEnumerable<Question>> ReturnQuestionDeltaAsync(
            this DbSet<Question> questions,
            IEnumerable<Question> newQuestions,
            CancellationToken cancellationToken = default)
        {
            if (!questions.Any())
                return newQuestions;
            return await Task.Run(() => newQuestions.ExceptBy(questions.Select(q => q.Content), q => q.Content), cancellationToken);
        }

        public static async Task<bool> QuestionExistsAsync(
            this DbSet<Question> questions,
            Question question,
            CancellationToken cancellationToken = default)
        {
            return await questions
                .Where(q => q.Content == question.Content)
                .CountAsync(cancellationToken) == 1;
        }
    }
}
