using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRh_Expense.Configs
{
    public class SwaggerConfig
    {

        public static void Register(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExpenseApi_SocleRh API", 
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Thanh NGUYEN",
                        Email = "nguyenthanhmary@yahoo.fr"
                    }
                
                });
            });
        }
    }
}
