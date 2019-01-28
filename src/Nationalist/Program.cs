using System;
using Microsoft.Extensions.DependencyInjection;
using Nationalist.Core;

namespace Nationalist
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServiceProvider();
            var modifier = serviceProvider.GetService<IModifier>();
            var generator = serviceProvider.GetService<Generator>();
            var locale = Interactions.GetTargetLocale();
            generator.GenerateList(locale, modifier);
        }

        static IServiceProvider ConfigureServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<NationalistSettings>();
            services.AddSingleton<ICldrProvider, CldrProvider>();
            services.AddSingleton<IGeoNamesProvider, GeoNamesProvider>();
            services.AddSingleton<IReducer, Reducer>();
            services.AddSingleton<IModifier, Modifier>();
            services.AddSingleton<CSharpGeneratorService>();
            services.AddSingleton<CsvGeneratorService>();
            services.AddSingleton<JsonGeneratorService>();
            services.AddSingleton<TsvGeneratorService>();
            services.AddSingleton<Generator>();

            return services.BuildServiceProvider();
        }
    }
}