using Application.Services;
using Application.Services.QuizManagment;
using Application.Services.QuoteManagment;
using Application.Services.UserManagment;
using Application.Services.UserQuizResultsManagment;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IQuizManagmentService, QuizManagmentService>();
            Services.AddScoped<IUserManagmentService, UserManagmentService>();
            Services.AddScoped<IQuoteManagementService, QuoteManagmentService>();
            Services.AddScoped<IUserQuizResultsManagmentService, UserQuizResultsManagmentService>();
        }

    }
}
